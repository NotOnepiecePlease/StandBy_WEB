using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace standby_data.Constantes
{
    public static class Querys
    {
        public static string ValorLucroUltimos7Dias =
            @"SELECT sum(sv_lucro) as LucroTotalSemana
	        FROM tb_servicos
	        WHERE sv_data BETWEEN 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 0) 
	        AND 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 7)";

        public static string ValorServicoUltimos7Dias =
            @"SELECT sum(sv_valorservico) as ServicoTotalSemana
	        FROM tb_servicos
	        WHERE sv_data BETWEEN 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 0) 
	        AND 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 7)";

        public static string ServicoUltimos7Dias = //--Deve ser GETDATE() no lugar da data
            @"WITH cte AS (
            SELECT CONVERT(DATE, sv_data) as sv_data, COUNT(*) as count
            FROM tb_servicos
            WHERE sv_data BETWEEN 
                DATEADD(week, DATEDIFF(week, 0, '01-05-2022')-1, 0) 
                AND 
                DATEADD(week, DATEDIFF(week, 0, '01-05-2022')-1, 7)
                GROUP BY CONVERT(DATE, sv_data)
            )

            SELECT DATENAME(WEEKDAY, sv_data) as day_of_week, count
            FROM cte
            ORDER BY sv_data";

        public static string ValorPrejuizoUltimos7Dias =
            @"SELECT sum(sv_lucro) as PrejuizoTotalSemana
	        FROM tb_servicos
	        WHERE sv_data BETWEEN 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 0) 
	        AND 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 7)
	        AND
	        sv_lucro < 0";

        public static string ValorPecasUltimos7Dias =
            @"SELECT sum(sv_valorpeca) as GastosPecasTotalSemana
	        FROM tb_servicos
	        WHERE sv_data BETWEEN 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 0) 
	        AND 
	        DATEADD(week, DATEDIFF(week, 0, GETDATE())-1, 7)";
    }
}