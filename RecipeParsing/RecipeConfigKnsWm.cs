using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeParsing
{
    public class RecipeConfigKnsWm
    {
        //public string RCP_MGR_CODE { get; set; }   // RcpFile MGR Code
        //public string ITEM_MGR_CODE { get; set; }  // Each Item MGR Code 
        public string GROUP_ID { get; set; }       // if WB_TYPE == P  null
        public string ITEM_ID { get; set; }
        public string ITEM_UNIT { get; set; }
        public string MASTER_ID { get; set; }       // if WB_TYPE == P  null
        public string VALID { get; set; }    // if WB_TYPE == P  null\
        public string WIRE_X_VALUE { get; set; }    // if WB_TYPE == P  null
        public string WIRE_Y_VALUE { get; set; }    // if WB_TYPE == P  null\

        public string X_LSL { get; set; }    // if WB_TYPE == P  null\
        public string RESULT_X_VALUE { get; set; }    // if WB_TYPE == P  null\
        public string X_USL { get; set; }    // if WB_TYPE == P  null\
        public string Y_LSL { get; set; }    // if WB_TYPE == P  null\
        public string RESULT_Y_VALUE { get; set; }    // if WB_TYPE == P  null\
        public string Y_USL { get; set; }    // if WB_TYPE == P  null\
        public string MASTER_X_VALUE { get; set; }       // if WB_TYPE == P  null
        public string MASTER_Y_VALUE { get; set; }       // if WB_TYPE == P  null
        public string WB_VALUE { get; set; }    // if WB_TYPE == P  null\
        public string COMPARE_YN { get; set; }
        public string FILE_NAME { get; set; }
        public string REVISION { get; set; }

        public RecipeConfigKnsWm() { }
        public RecipeConfigKnsWm(
                              //string RCP_MGR_CODE,
                              //string ITEM_MGR_CODE,
                              string GROUP_ID,
                              string ITEM_ID,
                              string ITEM_UNIT,
                              string MASTER_ID,
                              string MASTER_X_VALUE,
                              string MASTER_Y_VALUE,
                              string WIRE_X_VALUE,
                              string WIRE_Y_VALUE,
                              string RESULT_X_VALUE,
                              string RESULT_Y_VALUE,
                              string X_LSL,
                              string X_USL,
                              string Y_LSL,
                              string Y_USL,
                              string VALID,
                              string WB_VALUE,
                              string COMPARE_YN,
                              string FILE_NAME,
                              string REVISION)
        {
            //this.RCP_MGR_CODE = RCP_MGR_CODE;
            //this.ITEM_MGR_CODE = ITEM_MGR_CODE;
            this.GROUP_ID = GROUP_ID;
            this.ITEM_ID = ITEM_ID;
            this.ITEM_UNIT = ITEM_UNIT;
            this.MASTER_ID = MASTER_ID;
            this.MASTER_X_VALUE = MASTER_X_VALUE;
            this.MASTER_Y_VALUE = MASTER_Y_VALUE;
            this.WIRE_X_VALUE = WIRE_X_VALUE;
            this.WIRE_Y_VALUE = WIRE_Y_VALUE;
            this.RESULT_X_VALUE = RESULT_X_VALUE;
            this.RESULT_Y_VALUE = RESULT_Y_VALUE;
            this.X_LSL = X_LSL;
            this.X_USL = X_USL;
            this.Y_LSL = Y_LSL;
            this.Y_USL = Y_USL;
            this.VALID = VALID;
            this.WB_VALUE = WB_VALUE;
            this.COMPARE_YN = COMPARE_YN;
            this.FILE_NAME = FILE_NAME;
            this.REVISION = REVISION;
        }

        public void clear()
        {
            //this.RCP_MGR_CODE = null;
            //this.ITEM_MGR_CODE = null;
            this.GROUP_ID = null;
            this.ITEM_ID = null;
            this.ITEM_UNIT = null;
            this.MASTER_ID = null;
            this.MASTER_X_VALUE = null;
            this.MASTER_Y_VALUE = null;
            this.WIRE_X_VALUE = null;
            this.WIRE_Y_VALUE = null;
            this.RESULT_X_VALUE = null;
            this.RESULT_Y_VALUE = null;
            this.X_LSL = null;
            this.X_USL = null;
            this.Y_LSL = null;
            this.Y_USL = null;
            this.VALID = null;
            this.WB_VALUE = null;
            this.COMPARE_YN = null;
            this.FILE_NAME = null;
            this.REVISION = null;
        }
    }
}
