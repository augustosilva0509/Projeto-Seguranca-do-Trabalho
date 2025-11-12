using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public enum Weight
    {
        Null,
        Small,
        Medium,
        High
    }
    public enum Risk
    {
        Null,
        Small,
        Medium,
        High
    }
    public class Project
    {
        public string name;
        public int[] checkindexes;
        public bool used;
        public int p;
        public int c;
        public Weight weight;
        public Risk risk;
        public string imgName;

        public Project(string name, int[] checkindexes, int p, int c, Weight weight, Risk risk, string imgName)
        {
            this.name = name;
            this.checkindexes = checkindexes;
            this.used = false;
            this.p = p;
            this.c = c;
            this.weight = weight;
            this.risk = risk;
            this.imgName = imgName;
        }
        private static Project Null()
        {
            return new Project("Nulo", new int[] { -1 }, 0, 0, Weight.Null, Risk.Null, "black.png");
        }
        public static Project[] InitializeProjects()
        {
            Project[] projects = new Project[4];
            projects[0] = new Project("Escritório de Contabilidade", new int[] { 0, 1, 3 }, 1, 2, Weight.Small, Risk.Small, "cyan.png");
            projects[1] = new Project("Escritório A", new int[] { 0, 1, 2 , 3}, 3, 4, Weight.High, Risk.Small, "yellow.png");
            projects[2] = new Project("Escritório B", new int[] { 2, 3 }, 5, 6, Weight.Medium, Risk.Medium, "pink.png");
            projects[3] = new Project("Escritório C", new int[] { }, 7, 8, Weight.Small, Risk.High, "blue.png");
            return projects;
        }
        public static Project GetRandomProject(Project[] projects, int n)
        {
            if (n >= projects.Length) //Condição de parada, provavelmente temporario
                return Project.Null();

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
        public string RiskText()
        {
            switch (this.risk)
            {
                case Risk.Null:
                    return "Nulo";
                case Risk.Small:
                    return "Pequeno";
                case Risk.Medium:
                    return "Médio";
                case Risk.High:
                    return "Grave";
                default:
                    return "";
            }
        }
        public string WeightText()
        {
            switch (this.weight)
            {
                case Weight.Null:
                    return "Nulo";
                case Weight.Small:
                    return "Pequeno";
                case Weight.Medium:
                    return "Médio";
                case Weight.High:
                    return "Grande";
                default:
                    return "";
            }
        }
    }
}
