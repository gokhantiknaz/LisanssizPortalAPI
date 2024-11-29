using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Scripts
{
    public static class SqlQueryProvider
    {
        public static string GetQuery(SqlQuery query)
        {
            return query switch
            {
                SqlQuery.GunlukUretimTuketimMiktar => @"select ""Donem"",
       EXTRACT(day FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) as Gun,
       sum(""Uretim"")                                                              as ToplamUretim,
       sum(""CekisTuketim"")                                                        as ToplamTuketim
from ""AboneSaatlikEndeks""
where EXTRACT(YEAR FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) = EXTRACT(YEAR FROM CURRENT_DATE)
  and EXTRACT(MONTH FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) = EXTRACT(MONTH FROM CURRENT_DATE)
group by ""Donem"", EXTRACT(day FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD'));",

                SqlQuery.AboneAktifAylikTuketimUretim => @"
            WITH PreviousMonth AS (
                SELECT 
                    ""AboneId"",
                    ""EndexMonth"",
                    ""EndexYear"",
                    ""EndexType"",
                    ""T1Endex"" AS PreviousT1Endex,
                    ""T2Endex"" AS PreviousT2Endex,
                    ""T3Endex"" AS PreviousT3Endex,
  ""ReactiveInductive"" AS ""PreviousInduktifUsage"",
                    ""ReactiveCapasitive"" AS ""PreviousKapasitifUsage""
                FROM 
                    ""AboneEndeks""
                WHERE
                    1=1 and ""EndexType""=0  
                 {0}
            ),
            CurrentMonth AS (
                SELECT 
                    ""AboneId"",
                    ""EndexMonth"",
                    ""EndexYear"",
                    ""EndexType"",
                    ""T1Endex"",
                    ""T2Endex"",
                    ""T3Endex"",
  ""ReactiveInductive"",
                    ""ReactiveCapasitive""
                FROM 
                    ""AboneEndeks""
                WHERE 1=1
                  AND ""EndexType""=0 
                    {1}
            )
            SELECT 
                a.""Id"" as ""AboneId"",
                a.""Adi"" as ""Unvan"",
                c.""EndexMonth"",
                c.""EndexYear"",
                a.""SeriNo"",
                c.""EndexType"",
                COALESCE(c.""T1Endex"" - p.PreviousT1Endex, 0) * a.""Carpan"" AS ""T1Usage"",
                COALESCE(c.""T2Endex"" - p.PreviousT2Endex, 0) * a.""Carpan"" AS ""T2Usage"",
                COALESCE(c.""T3Endex"" - p.PreviousT3Endex, 0) * a.""Carpan"" AS ""T3Usage"",
 COALESCE(c.""ReactiveInductive"" - p.""PreviousInduktifUsage"", 0) * a.""Carpan"" AS ""InduktifUsage"",
  COALESCE(c.""ReactiveCapasitive"" - p.""PreviousKapasitifUsage"", 0) * a.""Carpan"" AS ""KapasitifUsage""
        
            FROM 
                CurrentMonth c inner join ""Abone"" a on a.""Id""= c.""AboneId""
            LEFT JOIN 
                PreviousMonth p ON c.""AboneId"" = p.""AboneId"";
        ",

                SqlQuery.YillikUretimTuketimToplam => @"
WITH RankedData AS (
    SELECT 
        ""AboneEndeks"".""EndexType"",  
        (MAX(""TSum"") - MIN(""TSum"")) * ""Carpan"" as Tuketim , 
        A.""Unvan"",
        ROW_NUMBER() OVER (PARTITION BY ""EndexType"" ORDER BY (MAX(""TSum"") - MIN(""TSum"")) * ""Carpan"" DESC) AS rn
    FROM ""AboneEndeks""
    INNER JOIN public.""Abone"" A ON ""AboneEndeks"".""AboneId"" = A.""Id""
    GROUP BY ""EndexType"", A.""Id"", A.""Unvan""
)
SELECT 
    ""EndexType"",
    CASE 
        WHEN rn <= 4 THEN ""Unvan"" 
        ELSE 'Diğer' 
    END AS ""Unvan"",
    SUM(Tuketim) AS TotalEndex
FROM RankedData
GROUP BY ""EndexType"", 
         CASE WHEN rn <= 4 THEN ""Unvan"" ELSE 'Diğer' END
ORDER BY ""EndexType"" DESC, ""Unvan"";",

                SqlQuery.YillikTuketimUretim => @"
         WITH EndeksFark AS (
    SELECT
        ""AboneId"",
        ""EndexMonth"",
        ""EndexYear"",
        ""EndexType"",
        LAG(""T1Endex"") OVER(PARTITION BY ""AboneId"", ""EndexType"" ORDER BY ""EndexYear"", ""EndexMonth"") AS PrevT1,
        LAG(""T2Endex"") OVER(PARTITION BY ""AboneId"", ""EndexType"" ORDER BY ""EndexYear"", ""EndexMonth"") AS PrevT2,
        LAG(""T3Endex"") OVER(PARTITION BY ""AboneId"", ""EndexType"" ORDER BY ""EndexYear"", ""EndexMonth"") AS PrevT3,
        ""T1Endex"",
        ""T2Endex"",
        ""T3Endex""
    FROM ""AboneEndeks""
    WHERE  ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE) OR (""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE) - 1 AND ""EndexMonth"" = 12)
)
SELECT
    ""EndexMonth"",
    ""EndexYear"",
    SUM(CASE
            WHEN ""EndexType"" = 1 THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)
                + ""T2Endex"" - COALESCE(PrevT2, 0)
                + ""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS Uretim,
    SUM(CASE
            WHEN ""EndexType"" = 0 AND ""Abone"".""MahsubaDahil"" = TRUE THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)
                + ""T2Endex"" - COALESCE(PrevT2, 0)
                + ""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS Tuketim,
    SUM(CASE
            WHEN ""EndexType"" = 0 AND ""Abone"".""MahsubaDahil"" = FALSE THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)
                + ""T2Endex"" - COALESCE(PrevT2, 0)
                + ""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS TuketimMahsubaDahilDegil,
       SUM(CASE
            WHEN ""EndexType"" = 0 THEN
                (""T1Endex"" - COALESCE(PrevT1, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS T1Tuketim,
         SUM(CASE
            WHEN ""EndexType"" = 0 THEN
                (""T2Endex"" - COALESCE(PrevT2, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS T2Tuketim,
         SUM(CASE
            WHEN ""EndexType"" = 0 THEN
                (""T3Endex"" - COALESCE(PrevT3, 0)) * ""Abone"".""Carpan""
            ELSE 0
        END) AS T3Tuketim

FROM EndeksFark
JOIN ""Abone"" ON EndeksFark.""AboneId"" = ""Abone"".""Id""
WHERE ""EndexYear"" = EXTRACT(YEAR FROM CURRENT_DATE)
GROUP BY ""EndexMonth"",""EndexYear""
ORDER BY ""EndexMonth"";
        ",

                SqlQuery.AyliEnDusukEnYUksekKullanimMiktarveGunleri =>
                 @"
WITH DailyConsumption AS (
    SELECT
        TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD') AS ""Day"",
        ""Donem"",
        SUM(""CekisTuketim"") AS ""TotalDailyConsumption""
    FROM
        ""AboneSaatlikEndeks""
     where    EXTRACT(YEAR FROM TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD')) = EXTRACT(YEAR FROM CURRENT_DATE)
    GROUP BY
        TO_DATE(SUBSTRING(""ProfilDate""::TEXT, 1, 8), 'YYYYMMDD'), ""Donem""
),
MonthlyHighLow AS (
    SELECT
        ""Donem"",
        MAX(""TotalDailyConsumption"") AS ""HighConsumption"",
        MIN(""TotalDailyConsumption"") AS ""LowConsumption""
    FROM
        DailyConsumption
    GROUP BY
        ""Donem""
)
SELECT
    mh.""Donem"",
    mh.""HighConsumption"",
    (SELECT ""Day""
     FROM DailyConsumption
     WHERE ""Donem"" = mh.""Donem"" AND ""TotalDailyConsumption"" = mh.""HighConsumption""
     LIMIT 1) AS ""HighDay"",
    mh.""LowConsumption"",
    (SELECT ""Day""
     FROM DailyConsumption
     WHERE ""Donem"" = mh.""Donem"" AND ""TotalDailyConsumption"" = mh.""LowConsumption""
     LIMIT 1) AS ""LowDay""
FROM
    MonthlyHighLow mh;
 ",
                _ => throw new ArgumentException("Unknown query")
            };
        }
    }
}
