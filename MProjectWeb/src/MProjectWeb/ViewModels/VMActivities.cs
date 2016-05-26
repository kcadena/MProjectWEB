using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MProjectWeb.ViewModels
{
    public class ActivityList
    {
        public long id_act { get; set; }
        public long id_characteristic { get; set; }
        public long? par_characteristic { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long folder { get; set; }
        public string state { get; set; }

        //type 1=>folders 2=>actividades de folders 3=>actividades de actividades
        public int type { get; set; }
    }

    public class ActivityInfo
    {
        ulong id_act { get; set; }
        ulong id_characteristic { get; set; }
        string name { get; set; }
        string description { get; set; }
        ulong id_fol { get; set; }
        string folder { get; set; }
        string state { get; set; }
        float asign_percent { get; set; }
        float execute_percent { get; set; }
        int time { get; set; }
        string type_time { get; set; }
        DateTime start_date { get; set; }
        uint sub_act_1_lev { get; set; }
        uint sub_act_all_lev { get; set; }
        bool prj_prj { get; set; }

    }
}
