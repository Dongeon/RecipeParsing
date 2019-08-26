using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeParsing
{
    public class Global
    {


        public static List<RecipeConfig> changeParaList = new List<RecipeConfig>();
        public static List<RecipeConfig> ParaList = new List<RecipeConfig>();

        public static List<RecipeConfigKnsPrm> KnsParaList = new List<RecipeConfigKnsPrm>();
        public static List<RecipeConfigKnsPrm> changeKnsParaList = new List<RecipeConfigKnsPrm>();
        public static List<RecipeConfigKnsWm> KnsWireList = new List<RecipeConfigKnsWm>();
        public static List<RecipeConfigKnsWm> changeKnsWireList = new List<RecipeConfigKnsWm>();


        public static string RecipeName = string.Empty;
        public static string FilePath = string.Empty;
    }
}
