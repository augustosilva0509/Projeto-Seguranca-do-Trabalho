using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Project
    {
        public string name;
        public int[] checkindexes;
        public bool used;

        public Project(string name, int[] checkindexes)
        {
            this.name = name;
            this.checkindexes = checkindexes;
            this.used = false;
        }
        public static Project[] InitializeProjects()
        {
            Project[] projects = new Project[4];
            projects[0] = new Project("Escritório de Contabilidade", new int[] { 0, 3, 5 });
            projects[1] = new Project("Escritório A", new int[] { 0, 1, 2 });
            projects[2] = new Project("Escritório B", new int[] { 5, 9 });
            projects[3] = new Project("Escritório C", new int[] { });
            return projects;
        }
        public static Project GetRandomProject(Project[] projects, int n)
        {
            if (n >= projects.Length) //Condição de parada, provavelmente temporario
                return new Project("Nulo", new int[] { -1 });

            Random random = new Random();
            int projectIndex = random.Next(projects.Length);
            while (projects[projectIndex].IsUsed())
            {
                projectIndex = random.Next(projects.Length);
            }
            return projects[projectIndex];
        }
        public bool IsUsed()
        {
            if(used) return true;
            else
            {
                used = true;
                return false;
            }
        }

    }
}
