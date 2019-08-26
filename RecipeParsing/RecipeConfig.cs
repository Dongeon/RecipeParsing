using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeParsing
{
    public class RecipeConfig
    {
        //public string RCP_MGR_CODE { get; set; }   // RcpFile MGR Code
        //public string ITEM_MGR_CODE { get; set; }  // Each Item MGR Code 
        public string WB_TYPE { get; set; }         // P : PARA , W : WIRE
        public string GROUP_ID { get; set; }
        public string ITEM_ID { get; set; }
        public string ITEM_UNIT { get; set; }
        public string PARA_VALUE { get; set; }      // if WB_TYPE == W  null
        public string PARA_sys_type { get; set; }      // if WB_TYPE == W  null
        public string PARA_param_type { get; set; }      // if WB_TYPE == W  null
        public string PARA_CLASS { get; set; }      // if WB_TYPE == W  null
        public string PARA_MIN { get; set; }      // if WB_TYPE == W  null
        public string PARA_MAX { get; set; }      // if WB_TYPE == W  null
        public string PARA_DEF { get; set; }      // if WB_TYPE == W  null  Para default
        public string FILE_NAME { get; set; }
        public string REVISION { get; set; }
        public string MASTER_ID { get; set; }       // if WB_TYPE == P  null
        public string MASTER_X_VALUE { get; set; }       // if WB_TYPE == P  null
        public string MASTER_Y_VALUE { get; set; }       // if WB_TYPE == P  null
        public string WIRE_X_VALUE { get; set; }    // if WB_TYPE == P  null
        public string WIRE_Y_VALUE { get; set; }    // if WB_TYPE == P  null
        public string COMPARE_YN { get; set; }
        //public Nullable<DateTime> MK_DATE { get; set; } // First init date
        //public Nullable<DateTime> UP_DATE { get; set; } // FILE UPDATE date


        public RecipeConfig() { }
        public RecipeConfig(
            //string RCP_MGR_CODE,
                              //string ITEM_MGR_CODE,
                              string WB_TYPE,
                              string GROUP_ID,
                              string ITEM_ID,
                              string ITEM_UNIT,
                              string PARA_VALUE,
                              string PARA_sys_type,
                              string PARA_param_type,
                              string PARA_CLASS,
                              string PARA_MIN,
                              string PARA_MAX,
                              string PARA_DEF,
                              string MASTER_ID,
                              string WIRE_X_VALUE,
                              string WIRE_Y_VALUE,
                              string COMPARE_YN,
                              string FILE_NAME,
                              //Nullable<DateTime> MK_DATE,
                              //Nullable<DateTime> UP_DATE,
                              string REVISION)
        {
            //this.RCP_MGR_CODE = RCP_MGR_CODE;
            //this.ITEM_MGR_CODE = ITEM_MGR_CODE;
            this.WB_TYPE = WB_TYPE;
            this.GROUP_ID = GROUP_ID;
            this.ITEM_ID = ITEM_ID;
            this.ITEM_UNIT = ITEM_UNIT;
            this.PARA_VALUE = PARA_VALUE;
            this.PARA_sys_type = PARA_sys_type;
            this.PARA_param_type = PARA_param_type;
            this.PARA_CLASS = PARA_CLASS;
            this.PARA_MIN = PARA_MIN;
            this.PARA_MAX = PARA_MAX;
            this.PARA_DEF = PARA_DEF;
            this.MASTER_ID = MASTER_ID;
            this.WIRE_X_VALUE = WIRE_X_VALUE;
            this.WIRE_Y_VALUE = WIRE_Y_VALUE;
            this.COMPARE_YN = COMPARE_YN;
            this.FILE_NAME = FILE_NAME;
            //this.MK_DATE = MK_DATE;
            //this.UP_DATE = MK_DATE;
            this.REVISION = REVISION;
        }

        public void clear()
        {
            //this.RCP_MGR_CODE = null;
            //this.ITEM_MGR_CODE = null;
            this.WB_TYPE = null;
            this.GROUP_ID = null;
            this.ITEM_ID = null;
            this.ITEM_UNIT = null;
            this.PARA_VALUE = null;
            this.PARA_MIN = null;
            this.PARA_MAX = null;
            this.PARA_DEF = null;
            this.PARA_sys_type = null;
            this.PARA_param_type = null;
            this.PARA_CLASS = null;
            this.MASTER_ID = null;
            this.WIRE_X_VALUE = null;
            this.WIRE_Y_VALUE = null;
            this.COMPARE_YN = null;
            this.FILE_NAME = null;
            //this.MK_DATE = null;
            //this.UP_DATE = null;
            this.REVISION = null;
        }

    }
}
