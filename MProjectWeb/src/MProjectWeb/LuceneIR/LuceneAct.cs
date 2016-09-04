using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




using Lucene.Net.Store;
using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;

using Lucene.Net.Util;
using Lucene.Net.Analysis.Snowball;
using System.IO;
using SF.Snowball.Ext;
using Contrib.Regex;



namespace MProjectWeb.LuceneIR
{

    public class LuceneAct
    {
        public int totDocs = 0;
        public int totSear = 0;
        private IndexWriter writer;
        public IndexSearcher searcher;
        private Lucene.Net.Store.Directory directory;
        private Analyzer analyzer;
        private SpanishStemmer sp;

        public LuceneAct()
        {
            //Setup indexer
            directory = FSDirectory.Open(@"D:\RepositoriosMProject\lucene\");
            analyzer = new SnowballAnalyzer(Lucene.Net.Util.Version.LUCENE_30, "Spanish");
            sp = new SpanishStemmer();
            sp.Stem();
        }

        public List<ScoreDoc> search(string text, string typ, string cars)
        {
            typ = "type:" + typ;
            totSear = 0;
            bool est = true;
            //Setup searcher
            try
            {
                searcher = new IndexSearcher(directory);
            }
            catch { est = false; }

            if (est)
            {
                MultiFieldQueryParser parser = new MultiFieldQueryParser(
                               Lucene.Net.Util.Version.LUCENE_30,
                               new string[] { "nom", "desc", "cont" },
                               analyzer
                               );
                //QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "<default field>", analyzer);

                IndexReader rea = IndexReader.Open(directory, false);
                totDocs = rea.MaxDoc;
                rea.Dispose();
                //Supply conditions
                parser.AllowLeadingWildcard = true;

                //Do the search
                Query query;
                if (text.Equals(""))
                {
                    if (cars.Length > 0)
                        query = parser.Parse(cars + " AND " + typ);
                    else
                        query = parser.Parse(typ);
                }

                else
                {
                    string[] car = new string[] { "+", "-", "&&", "||", "!", "(", ")", "{",
                        "}", "[", "]", "^", "\"", "~", "*", "?", ":", "\\", "AND", "OR" };
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text.ElementAt(i) == ' ')
                        {
                            try
                            {
                                if (text.ElementAt(i + 1) == ' ')
                                {
                                    text = text.Remove(i, 1);
                                    i--;
                                }
                            }
                            catch
                            {
                                text = text.Remove(i--, 1);
                            }

                        }

                        foreach (var x in car)
                        {
                            if (text.ElementAt(i).Equals(x))
                            {
                                char s = '\\';
                                text = text.Insert(i, s.ToString());
                                i++;
                            }
                        }
                    }
                    parser.FuzzyMinSim = (float)0.8;
                    if (cars.Length > 0)
                        query = parser.Parse(cars + " AND " + typ + " AND " + "(\"" + text + "\"~)");
                    else
                        query = parser.Parse(typ + " AND " + "(\"" + text + "\"~)");

                }
                try
                {
                    TopDocs hits = searcher.Search(query, totDocs);
                    if (hits.TotalHits == 0)
                    {
                        parser.FuzzyMinSim = (float)0.6;
                        text = text + "~";
                        text = text.Replace(" ", "~ ");

                        //System.Windows.MessageBox.Show(text + "    " + text.Length);

                        if (text.Length > 1)
                            query = parser.Parse(typ + " AND " + "(" + text + ")" + " AND " + cars);
                        else
                            query = parser.Parse(typ + " AND " + cars);
                        hits = searcher.Search(query, totDocs);
                    }

                    List<ScoreDoc> sc = hits.ScoreDocs.ToList<ScoreDoc>();
                    totSear = sc.Count;
                    return sc;
                }
                catch (Exception err)
                {
                    //System.Windows.MessageBox.Show(err.ToString());
                }
            }
            return null;
        }


    }
}


/*

"dependencies": {
        "Lucene.Net": "3.0.3",
        "Lucene.Net.Contrib": "3.0.3"
    }
*/
