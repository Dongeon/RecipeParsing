using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeParsing
{
    public class RecipeConfigKnsPrm
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

        public RecipeConfigKnsPrm() { }
        public RecipeConfigKnsPrm(
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
                              string FILE_NAME,
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
            this.FILE_NAME = FILE_NAME;
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
            this.FILE_NAME = null;
            this.REVISION = null;
        }
    }
}
