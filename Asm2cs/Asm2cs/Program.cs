using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asm2cs;

public class Program
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader(args[1]);

        KnowledgeBase kb = new KnowledgeBase(reader);

        // Print the contents of the KnowledgeBase
        Console.WriteLine(kb.Print());
        reader.ReadLine();
        string query = reader.ReadLine();
        Console.WriteLine("Query: " + query);
        Console.WriteLine();
        Algorithm[] algorithms = { new ForwardChaining(kb, query), new BackwardChaining(kb, query), new TruthTable(kb, query) };
        int i = 0;
        bool algorithmsSearch = true;
        while (i < algorithms.Length && algorithmsSearch)
        {
            if (algorithms[i].GetCode().Equals(args[0]))
            {
                algorithmsSearch = false;
                Console.WriteLine(algorithms[i].GetCode() + " search: ");
                Console.WriteLine("> " + algorithms[i].CheckQuery());
            }
            i++;
        }
    }
}