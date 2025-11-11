using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public class CaseResult
    {
        public Project caseProject;
        public int[] errors;
        public CaseResult(Project caseProject, int[] errors)
        {
            this.caseProject = caseProject;
            this.errors = errors;
        }
    }
    public class Result
    {
        public CaseResult[] cases;
        private int counter;
        public Result(int numCases)
        {
            this.cases = new CaseResult[numCases];
            this.counter = 0;
        }
        public void UpdateCase(Project caseProject, int[] errors)
        {
            if(counter >= cases.Length)
                return;
            this.cases[counter] = new CaseResult(caseProject, errors);
            counter++;
        }
    }
}
