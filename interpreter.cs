using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class interpreter : Form
    {
        bool stop = true;

        public interpreter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                codeTextBox.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private List<Regex> loadReservedWords()
        {
            List<Regex> reserved = new List<Regex>();
            reserved.Add(new Regex(@"^HAI$"));
            reserved.Add(new Regex(@"^KTHXBYE$"));
            reserved.Add(new Regex(@"^BTW$"));
            reserved.Add(new Regex(@"^OBTW$"));
            reserved.Add(new Regex(@"^TLDR$"));
            reserved.Add(new Regex(@"^ITZ$"));
            reserved.Add(new Regex(@"^R$"));
            reserved.Add(new Regex(@"^NOT$"));
            reserved.Add(new Regex(@"^DIFFRINT$"));
            reserved.Add(new Regex(@"^SMOOSH$"));
            reserved.Add(new Regex(@"^MKAY$"));
            reserved.Add(new Regex(@"^VISIBLE$"));
            reserved.Add(new Regex(@"^GIMMEH$"));
            reserved.Add(new Regex(@"^OIC$"));
            reserved.Add(new Regex(@"^WTF\?$"));
            reserved.Add(new Regex(@"^OMG$"));
            reserved.Add(new Regex(@"^OMGWTF$"));
            reserved.Add(new Regex(@"^GTFO$"));
            reserved.Add(new Regex(@"^UPPIN$"));
            reserved.Add(new Regex(@"^NERFIN$"));
            reserved.Add(new Regex(@"^AN$"));
            reserved.Add(new Regex(@"^SUM$"));
            reserved.Add(new Regex(@"^DIFF$"));
            reserved.Add(new Regex(@"^PRODUKT$"));
            reserved.Add(new Regex(@"^QUOSHUNT$"));
            reserved.Add(new Regex(@"^MOD$"));
            reserved.Add(new Regex(@"^BIGGR$"));
            reserved.Add(new Regex(@"^SMALLR$"));
            reserved.Add(new Regex(@"^BOTH$"));
            reserved.Add(new Regex(@"^EITHER$"));
            reserved.Add(new Regex(@"^WON$"));
            reserved.Add(new Regex(@"^ALL$"));
            reserved.Add(new Regex(@"^ANY$"));
            reserved.Add(new Regex(@"^BOTH$"));
            reserved.Add(new Regex(@"^O$"));
            reserved.Add(new Regex(@"^YA$"));
            reserved.Add(new Regex(@"^NO$"));
            reserved.Add(new Regex(@"^OF$"));
            reserved.Add(new Regex(@"^SAEM$"));
            reserved.Add(new Regex(@"^RLY\?$"));
            reserved.Add(new Regex(@"^RLY$"));
            reserved.Add(new Regex(@"^I$")); //I HAS A 
            reserved.Add(new Regex(@"^IS$")); //IS NOW A
            reserved.Add(new Regex(@"^IM$"));
            reserved.Add(new Regex(@"^WIN$"));
            reserved.Add(new Regex(@"^FAIL$"));
            reserved.Add(new Regex(@"^O RLY\?$"));
            reserved.Add(new Regex(@"^I HAS$")); //I HAS A
            return reserved;
        }

        private Dictionary<string, Regex> loadLiterals()
        {
            Dictionary<string, Regex> literalsDictionary = new Dictionary<string, Regex>(); //instantiate new dictionary of lexemes
            literalsDictionary.Add("Numbr Literal", new Regex(@"[-+]?^\d+$"));
            literalsDictionary.Add("Numbar Literal", new Regex(@"[-+]?^(\d+.\d+$)"));
            literalsDictionary.Add("Yarn Literal", new Regex(@"^"".*"""));
            literalsDictionary.Add("Troof Literal", new Regex(@"(WIN|FAIL)"));
            literalsDictionary.Add("Type Literal", new Regex(@"(YARN|NUMBR|NUMBAR|TROOF|NOOB)"));
            return literalsDictionary;
        }

        private Dictionary<string, Regex> loadLexemes()
        {
            Dictionary<string, Regex> lexemesDictionary = new Dictionary<string, Regex>(); //instantiate new dictionary of lexemes
            lexemesDictionary.Add("Code Delimiter - Start", new Regex(@"^HAI$"));
            lexemesDictionary.Add("Code Delimiter - End", new Regex(@"^KTHXBYE$"));
            lexemesDictionary.Add("Comment Line", new Regex(@"^BTW$"));
            lexemesDictionary.Add("Comment Delimiter - Start", new Regex(@"^OBTW$"));
            lexemesDictionary.Add("Comment Delimiter - End", new Regex(@"^TLDR$"));
            lexemesDictionary.Add("Value Initialization", new Regex(@"^ITZ$"));
            lexemesDictionary.Add("Value Assignment", new Regex(@"^R$"));
            lexemesDictionary.Add("NOT Operator", new Regex(@"^NOT$"));
            lexemesDictionary.Add("Comparison Operator", new Regex(@"^DIFFRINT$"));
            lexemesDictionary.Add("Concatenator", new Regex(@"^SMOOSH$"));
            lexemesDictionary.Add("Infinite Arity Delimiter", new Regex(@"^MKAY$")); // paayos lol
            lexemesDictionary.Add("Output Keyword", new Regex(@"^VISIBLE$"));
            lexemesDictionary.Add("Input Keyword", new Regex(@"^GIMMEH$"));
            lexemesDictionary.Add("If-Else/Switch Delimiter - End", new Regex(@"^OIC$"));
            lexemesDictionary.Add("Switch Case Delimiter - Start", new Regex(@"^WTF\?$"));
            lexemesDictionary.Add("Switch Case", new Regex(@"^OMG$"));
            lexemesDictionary.Add("Default Case", new Regex(@"^OMGWTF$"));
            lexemesDictionary.Add("Break", new Regex(@"^GTFO$"));
            lexemesDictionary.Add("Yr", new Regex(@"^YR$")); // paayos to lol
            lexemesDictionary.Add("Extra Keyword", new Regex(@"^AN$"));
            lexemesDictionary.Add("IT", new Regex(@"^IT$"));
            lexemesDictionary.Add("Addition Arithmetic Operator", new Regex(@"^SUM OF$"));
            lexemesDictionary.Add("Subtraction Arithmetic Operator", new Regex(@"^DIFF OF$"));
            lexemesDictionary.Add("Multiplication Arithmetic Operator", new Regex(@"^PRODUKT OF$"));
            lexemesDictionary.Add("Division Arithmetic Operator", new Regex(@"^QUOSHUNT OF$"));
            lexemesDictionary.Add("Modulo Operator", new Regex(@"^MOD OF$"));
            lexemesDictionary.Add("Greater than Comparison Operator", new Regex(@"^BIGGR OF$"));
            lexemesDictionary.Add("Less than Comparison Operator", new Regex(@"^SMALLR OF$"));
            lexemesDictionary.Add("AND Operator", new Regex(@"^BOTH OF$"));
            lexemesDictionary.Add("OR Operator", new Regex(@"^EITHER OF$"));
            lexemesDictionary.Add("XOR Operator", new Regex(@"^WON OF$"));
            lexemesDictionary.Add("Infinite Arity AND", new Regex(@"^ALL OF$"));
            lexemesDictionary.Add("Infinite Arity OR", new Regex(@"^ANY OF$"));
            lexemesDictionary.Add("Equality Comparison Operator", new Regex(@"^BOTH SAEM$"));
            lexemesDictionary.Add("Else-if Delimiter - Start", new Regex(@"^O RLY\?$"));
            lexemesDictionary.Add("If Statement", new Regex(@"^YA RLY$"));
            lexemesDictionary.Add("Else Statement", new Regex(@"^NO WAI$"));
            lexemesDictionary.Add("Variable Declaration", new Regex(@"^I HAS A$"));
            return lexemesDictionary; //return the dictionary of lexemes
        }

        private void readCode() //will read the code in the textbox
        {
            string[] lines = codeTextBox.Text.Split('\n'); //split the code by line
            Dictionary<string, Regex> shortLexemes = loadLexemes(); //load dictionary of lexemes
            Dictionary<string, Regex> literals = loadLiterals(); //load dictionary of lexemes
            List<Regex> reservedWords = loadReservedWords();
            Regex variable = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            string token = "";
            int appendFlag = 0;
            int breakFlag1 = 0;
            int breakFlag2 = 0;

            if (!codeTextBox.Text.Contains("HAI") || !codeTextBox.Text.Contains("KTHXBYE")) //if the first line has HAI and last has KTHXBYE
            {
                richTextBox2.Text += "Program must start with HAI and end with KTHXBYE"; //get out of loop
                return;
            }

            for (int i = 0; i < lines.Length; i++) //iterate per existing line
            {
                string[] words = lines[i].Split(' '); //split per line by space

                for (int j = 0; j < words.Length; j++) //iterate per word in the line
                {
                    if (appendFlag == 1)
                    {
                        token = token + " " + words[j]; //appends unmathced token from previous iteration
                        appendFlag = 0;
                    }
                    else
                    {
                        token = words[j]; //gets specific token
                    }

                    if (breakFlag1 == 1)
                    {
                        breakFlag1 = 0;
                        break;
                    }
                    else if (breakFlag2 == 1)
                    {
                        if (token != "TLDR")
                        {
                            continue;
                        }
                        else
                        {
                            breakFlag2 = 0;
                        }
                    }

                    foreach (KeyValuePair<string, Regex> regex in shortLexemes)
                    {
                        if (regex.Value.IsMatch(token)) //if matched with regex
                        {
                            if (token == "BTW")
                            {
                                breakFlag1 = 1;
                            }
                            else if (token == "OBTW")
                            {
                                breakFlag2 = 1;
                            }
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(dataGridView1);
                            row.Cells[0].Value = token;
                            row.Cells[1].Value = regex.Key;
                            dataGridView1.Rows.Add(row);
                            token = "";
                            appendFlag = 0;
                            break;
                        }
                        else //if unmatched
                        {
                            foreach (Regex res in reservedWords)
                            {
                                if (res.IsMatch(token))
                                {
                                    appendFlag = 1;
                                }
                            }

                            if (appendFlag == 0 && token != "" && variable.IsMatch(token))
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dataGridView1);
                                row.Cells[0].Value = token;
                                row.Cells[1].Value = "Variable";
                                dataGridView1.Rows.Add(row);
                                appendFlag = 0;
                                break;
                            }
                            else if (token != "")
                            {
                                appendFlag = 1;
                            }
                        }
                    }

                    foreach (KeyValuePair<string, Regex> lit in literals)
                    {
                        if (lit.Value.IsMatch(token))
                        {
                            string[] y = { };

                            if (lit.Key == "Yarn Literal") //if yarn literal
                            {
                                y = token.Split('\"');
                                DataGridViewRow row1 = new DataGridViewRow();
                                row1.CreateCells(dataGridView1);
                                row1.Cells[0].Value = "\"";
                                row1.Cells[1].Value = "String Delimiter";
                                dataGridView1.Rows.Add(row1);

                                DataGridViewRow row2 = new DataGridViewRow();
                                row2.CreateCells(dataGridView1);
                                row2.Cells[0].Value = y[1];
                                row2.Cells[1].Value = lit.Key;
                                dataGridView1.Rows.Add(row2);

                                DataGridViewRow row3 = new DataGridViewRow();
                                row3.CreateCells(dataGridView1);
                                row3.Cells[0].Value = "\"";
                                row3.Cells[1].Value = "String Delimiter";
                                dataGridView1.Rows.Add(row3);

                                token = "";
                                appendFlag = 0;
                                break;
                            }
                            else
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(dataGridView1);
                                row.Cells[0].Value = token;
                                row.Cells[1].Value = lit.Key;
                                dataGridView1.Rows.Add(row);
                                token = "";
                                appendFlag = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void readLexemesTable()
        {
            Stack arithmetic = new Stack();
            Stack concatenation = new Stack();
            int initialized = 0;
            int rowIndex = 0;
            int ihasaFlag = 0;
            int assignment = 0;
            int outputFlag = 0;
            int inputFlag = 0;
            int varCount = 0;
            int varFlag = 0;
            int arithmeticFlag = 0;
            int currVarIndex = 0;
            int noErrFlag = 0;
            int varFlag2 = 0;
            int ihasaCount = 0;
            int itFlag = 0;
            int compFlag = 0;
            int stringFlag = 0;
            int delimiter = 0;
            int computeFlag = 0;
            int concatenateFlag = 0;
            int nextString = 0;
            int smooshDone = 0;
            int notFlag = 0;
            int assignComputeFlag = 0;
            int operationFlag = 0;
            int arityFlag = 0;
            int endArity = 0;
            int ifElseFlag = 0;
            string itValue = "";
            string caseValue = "";
            int yarlyFlag = 0;
            int prevYaRlyFlag = 0;
            int nowaiFlag = 0;
            int stringDelimiter = 0;
            int switchFlag = 0;
            int omgFlag = 0;
            int stopFlag = 0;
            int conditionFlag = 0;
            int switchDone = 0;
            int found = 0;
            int varInLine = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows) //iterate per row in datagridview of lexemes table
            {
                if (stopFlag == 0 && omgFlag == 1 && found == 1 && Convert.ToString(row.Cells[1].Value) == "Break")
                {//GTFO
                    omgFlag = 0;
                    found = 0;
                    switchDone = 1;
                    continue;
                }
                else if (stopFlag == 0 && omgFlag == 1 && found == 1 && Convert.ToString(row.Cells[1].Value) != "Break")
                {
                    omgFlag = 1;
                    found = 1;
                    switchDone = 0;
                }

                if (Convert.ToString(row.Cells[1].Value) == "Switch Case" && omgFlag == 0 && switchFlag == 1 && found == 0)
                { //if OMG is found, right block not found
                    stopFlag = 0;
                    omgFlag = 1;
                    conditionFlag = 1;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Switch Case" && omgFlag == 1 && switchFlag == 1 && found == 0)
                { //OMG without break, right not found
                    stopFlag = 0;
                    omgFlag = 1;
                    conditionFlag = 1;
                    continue;
                }

                if (stopFlag == 1 && omgFlag == 1 && conditionFlag == 0 && Convert.ToString(row.Cells[1].Value) != "Break" && switchFlag == 1)
                { //skip everything
                    continue;
                }
                else if (stopFlag == 1 && omgFlag == 1 && conditionFlag == 0 && Convert.ToString(row.Cells[1].Value) == "Break" && switchFlag == 1 && switchDone == 0)
                { //GTFO
                    stopFlag = 0;
                    omgFlag = 0;
                    conditionFlag = 0;
                }

                if (switchDone == 1 && Convert.ToString(row.Cells[1].Value) != "If-Else/Switch Delimiter - End")
                { //not OIC, right block is found, working
                    continue;
                }
                else if (switchDone == 1 && Convert.ToString(row.Cells[1].Value) == "If-Else/Switch Delimiter - End")
                { //OIC if found; end, working
                    switchFlag = 0;
                    omgFlag = 0;
                    stopFlag = 0;
                    conditionFlag = 0;
                    switchDone = 0;
                    found = 0;
                }

                if (ihasaFlag == 1 && Convert.ToString(row.Cells[1].Value) != "Value Initialization" && ihasaCount == 2 && assignComputeFlag == 0) //for uninitialized variable
                {
                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(dataGridView2);
                    row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                    row1.Cells[1].Value = "NOOB";

                    dataGridView2.Rows.Add(row1);

                    initialized = 0;
                    varFlag2 = 0;
                    ihasaFlag = 0;
                    ihasaCount = 0;
                    currVarIndex = 0;
                }
                else if (ihasaFlag == 1 && Convert.ToString(row.Cells[1].Value) != "Variable" && ihasaCount == 1)
                {
                    richTextBox2.Text += "Error";
                    return;
                }
                else if (arityFlag == 0 && endArity == 1)
                {
                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(dataGridView2);
                    if (itFlag == 1)
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row2.Cells[0].Value) == "IT") //if value of variable is found
                            { 
                                dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                                break;
                            }
                        }
                         row1.Cells[0].Value = "IT";
                    }
                    else
                    {
                        row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                    }

                    row1.Cells[1].Value = arithmetic.Pop();
                    dataGridView2.Rows.Add(row1);
                    endArity = 0;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "String Delimiter" && stringDelimiter == 1 && outputFlag == 1)
                {  
                    outputFlag = 0;
                    stringDelimiter = 0;
                    continue;
                }

                if (ifElseFlag == 1)
                {
                    arithmeticFlag = 0;
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {

                        if (Convert.ToString(row2.Cells[0].Value) == "IT")
                        {
                            itValue = Convert.ToString(row2.Cells[1].Value);
                            break;
                        }
                    }

                    if (itValue == "WIN")
                    {
                        if (Convert.ToString(row.Cells[1].Value) == "If Statement")
                        {
                            yarlyFlag = 1;
                            prevYaRlyFlag = 1;
                            ifElseFlag = 0;
                            itFlag = 0;
                            continue;
                        }
                    }
                    else if (itValue == "FAIL")
                    {
                        if (Convert.ToString(row.Cells[1].Value) == "Else Statement")
                        {
                            nowaiFlag = 1;
                            ifElseFlag = 0;
                            itFlag = 0;
                            continue;
                        } else if (Convert.ToString(row.Cells[1].Value) == "If-Else/Switch Delimiter - End")
                        {
                            ifElseFlag = 0;
                            itFlag = 0;
                            continue;
                        } else
                        {
                        continue;
                        }
                    }
                }
                if (yarlyFlag == 1 && (Convert.ToString(row.Cells[1].Value) == "Else Statement"))
                {
                    yarlyFlag = 0;
                }
                else if (yarlyFlag == 1 && Convert.ToString(row.Cells[1].Value) == "If-Else/Switch Delimiter - End")
                {
                    yarlyFlag = 0;
                    prevYaRlyFlag = 0;
                    continue;
                }
                else if (nowaiFlag == 1 && Convert.ToString(row.Cells[1].Value) == "If-Else/Switch Delimiter - End")
                {
                    nowaiFlag = 0;
                    continue;
                }

                if (yarlyFlag == 0 && prevYaRlyFlag == 1 && Convert.ToString(row.Cells[1].Value) != "If-Else/Switch Delimiter - End" )
                {
                    continue;
                }
                else if (yarlyFlag == 0 && prevYaRlyFlag == 1 && Convert.ToString(row.Cells[1].Value) == "If-Else/Switch Delimiter - End")
                {
                    prevYaRlyFlag = 0;
                }

                
                if (Convert.ToString(row.Cells[1].Value) == "Variable Declaration") //if there is a variable declaration
                {
                    ihasaFlag = 1;
                    ihasaCount += 1;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Variable")
                {
                    if (outputFlag == 0 && ihasaFlag == 0 && assignment == 0 && arithmeticFlag == 0 && inputFlag == 0 && concatenateFlag == 0) //there is a variable at the start of the line
                    {
                        varFlag = 1;
                        varInLine = 1;
                        currVarIndex = row.Index;
                        continue;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Output Keyword") //if there is something to be printed
                {
                    outputFlag = 1;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Input Keyword") //if gimmeh is found
                {

                    inputFlag = 1;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Concatenator" && ihasaFlag == 0 && assignment == 0) //if smoosh is found
                {
                    itFlag = 1;
                    concatenateFlag = 1;
                    nextString = 1;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Addition Arithmetic Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Subtraction Arithmetic Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Multiplication Arithmetic Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Division Arithmetic Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Modulo Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Greater than Comparison Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Less than Comparison Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Comparison Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Equality Comparison Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                    operationFlag = 1;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "AND Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "OR Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "XOR Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "NOT Operator" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity AND" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity OR" && varFlag == 0 && inputFlag == 0 && ihasaFlag == 0)
                {
                    if (itFlag == 0)
                    {
                        assignment = 1;
                    }
                    itFlag = 1; //something is to be stored in IT variable
                    if (outputFlag == 1)
                    {
                        computeFlag = 1;
                    }
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Extra Keyword" && arityFlag == 1 && operationFlag == 0)
                {
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity Delimiter" && arityFlag == 1)
                {
                    endArity = 1;
                    arityFlag = 0;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Else-if Delimiter - Start")
                {
                    ifElseFlag = 1;
                    continue;
                }
                else if (Convert.ToString(row.Cells[1].Value) == "Switch Case Delimiter - Start")
                { //WTF? is found
                    switchFlag = 1;
                    continue;
                }

                if (varFlag == 1)
                {
                    if (Convert.ToString(row.Cells[1].Value) == "Value Assignment") //<variable> R
                    {
                        assignment = 1;
                        continue;   
                    }
                }

                if (varInLine == 1 && Convert.ToString(row.Cells[1].Value) != "Value Assignment" && assignment == 0) {
                    varInLine = 0;
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (Convert.ToString(row2.Cells[0].Value) == "IT") //if value of variable is found
                        {
                            dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                            break;
                        }
                    }
                    foreach (DataGridViewRow row2 in dataGridView2.Rows) {
                        if(Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value) == Convert.ToString(row2.Cells[0].Value)) {
                            noErrFlag = 1;
                            DataGridViewRow row1 = new DataGridViewRow();
                            row1.CreateCells(dataGridView2);
                            row1.Cells[0].Value = "IT";
                            row1.Cells[1].Value = row2.Cells[1].Value;
                            dataGridView2.Rows.Add(row1);
                            break;
                        }
                    }
                    if(noErrFlag == 0) {
                        richTextBox2.Text += "Undeclared";
                        return;
                    }
                }

                if (switchFlag == 1)
                { //WTF? is found
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {

                        if (Convert.ToString(row2.Cells[0].Value) == "IT")
                        {
                            caseValue = Convert.ToString(row2.Cells[1].Value).Trim('"'); //eliminate double quotes from yan literal in symbol table
                            break;
                        }
                    }
                    if (omgFlag == 1 && stopFlag == 0 && conditionFlag == 1)
                    {
                        conditionFlag = 0;
                        
                        if (Convert.ToString(row.Cells[1].Value) == "String Delimiter")
                        {
                            omgFlag = 1;
                            stopFlag = 0;
                            conditionFlag = 1;
                            continue; //skip to next token
                        }

                        if (Convert.ToString(row.Cells[0].Value) != caseValue)
                        {
                            stopFlag = 1;
                            omgFlag = 1;
                            continue;
                        }
                        else
                        {
                            found = 1;
                            stopFlag = 0;
                            omgFlag = 1;
                        }
                    }
                }

                if (ihasaFlag == 1)
                {
                    if (Convert.ToString(row.Cells[1].Value) == "Variable" && ihasaCount == 1)
                    {
                        currVarIndex = row.Index;
                        ihasaCount += 1;
                        varFlag2 = 1;
                        continue;
                    }

                    if (varFlag2 == 1)
                    {
                        if (Convert.ToString(row.Cells[1].Value) == "Value Initialization")
                        {
                            ihasaCount += 1;
                            initialized = 1;
                            continue;
                        }
                        if (initialized == 1) //if initial value is an expression
                        {
                            if (Convert.ToString(row.Cells[1].Value) == "String Delimiter")
                            {
                                stringFlag = 1;
                                continue;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Addition Arithmetic Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Subtraction Arithmetic Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Multiplication Arithmetic Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Division Arithmetic Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Modulo Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Greater than Comparison Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Less than Comparison Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Comparison Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Equality Comparison Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "AND Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                operationFlag = 1;
                                varFlag2 = 0;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "OR Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignment = 1;
                                varFlag2 = 0;
                                operationFlag = 1;
                                assignComputeFlag = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "XOR Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                operationFlag = 1;
                                varFlag2 = 0;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "NOT Operator" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity AND" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity OR" && varFlag == 0 && inputFlag == 0)
                            {
                                assignComputeFlag = 1;
                                assignment = 1;
                                varFlag2 = 0;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Concatenator" && varFlag == 0 && inputFlag == 0)
                            {
                                initialized = 0;
                                assignment = 1;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Troof Literal" && varFlag == 0 && inputFlag == 0)
                            {
                                DataGridViewRow row1 = new DataGridViewRow();
                                row1.CreateCells(dataGridView2);
                                row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                                row1.Cells[1].Value = row.Cells[0].Value;
                                dataGridView2.Rows.Add(row1);

                                initialized = 0;
                                varFlag2 = 0;
                                ihasaFlag = 0;
                                ihasaCount = 0;
                                currVarIndex = 0;
                                continue;
                            }
                            else if (Convert.ToString(row.Cells[1].Value) == "Variable" && varFlag == 0 && inputFlag == 0)
                            {
                                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                                {

                                    if (Convert.ToString(row.Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                                    {
                                        DataGridViewRow row1 = new DataGridViewRow();
                                        row1.CreateCells(dataGridView2);
                                        row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                                        row1.Cells[1].Value = row2.Cells[1].Value;

                                        dataGridView2.Rows.Add(row1);
                                        noErrFlag = 1;
                                        break;
                                    }
                                }
                                initialized = 0;
                                varFlag2 = 0;
                                ihasaFlag = 0;
                                ihasaCount = 0;
                                currVarIndex = 0;
                                continue;
                            }
                            else
                            {
                                
                                DataGridViewRow row1 = new DataGridViewRow();
                                row1.CreateCells(dataGridView2);
                                row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                                if (stringFlag == 1)
                                {
                                    row1.Cells[1].Value = "\"" + row.Cells[0].Value + "\"";
                                    stringFlag = 0;
                                }
                                else
                                {
                                    row1.Cells[1].Value = row.Cells[0].Value;
                                }

                                dataGridView2.Rows.Add(row1);

                                initialized = 0;
                                varFlag2 = 0;
                                ihasaFlag = 0;
                                ihasaCount = 0;
                                currVarIndex = 0;
                                continue;
                            }
                        }
                    }
                }

                
                if (assignment == 1) //if there is an assignment to a variable
                {
                    if (itFlag == 0 && ihasaFlag == 0) //if no need to store in IT
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {

                            if (Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                            {
                                noErrFlag = 1;
                                break;
                            }
                        }

                        if (noErrFlag == 0)
                        {
                            richTextBox2.Text += "Error. Undeclared variable.";
                            return;
                        }
                    }

                    assignment = 0;
                    if (Convert.ToString(row.Cells[1].Value) == "Addition Arithmetic Operator")
                    {
                        arithmetic.Push("+");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Subtraction Arithmetic Operator")
                    {
                        arithmetic.Push("-");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Multiplication Arithmetic Operator")
                    {
                        arithmetic.Push("*");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Division Arithmetic Operator")
                    {
                        arithmetic.Push("/");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Modulo Operator")
                    {
                        arithmetic.Push("%");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Greater than Comparison Operator")
                    {
                        arithmetic.Push(">");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Less than Comparison Operator")
                    {
                        arithmetic.Push("<");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Comparison Operator")
                    {
                        arithmetic.Push("diffrint");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Equality Comparison Operator")
                    {
                        arithmetic.Push("bothsaem");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "AND Operator")
                    {
                        arithmetic.Push("and");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "OR Operator")
                    {
                        arithmetic.Push("or");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "XOR Operator")
                    {
                        arithmetic.Push("xor");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "NOT Operator")
                    {
                        arithmetic.Push("not");
                        arithmeticFlag = 1;
                        operationFlag = 1;
                        notFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity AND")
                    {
                        arithmetic.Push("all of");
                        arithmeticFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Infinite Arity OR")
                    {
                        arithmetic.Push("any of");
                        arithmeticFlag = 1;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Concatenator")
                    {
                        concatenateFlag = 1;
                        nextString = 1;
                        continue;
                    }
                    else
                    {
                        if (Convert.ToString(row.Cells[1].Value) == "String Delimiter")
                        {
                            assignment = 1;
                            continue;
                        }
                        else
                        {
                            foreach (DataGridViewRow row2 in dataGridView2.Rows)
                            {

                                if (Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                                {
                                    dataGridView2.Rows.RemoveAt(row2.Index); // delete existing row
                                    break;
                                }
                            }

                            DataGridViewRow row1 = new DataGridViewRow();
                            row1.CreateCells(dataGridView2);
                            row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                            row1.Cells[1].Value = row.Cells[0].Value;

                            dataGridView2.Rows.Add(row1);
                            varFlag = 0;
                            currVarIndex = 0;
                        }
                        continue;
                    }
                }
                if (arithmeticFlag == 1)
                {
                    if (Convert.ToString(row.Cells[1].Value) == "Variable") //find the variable in the symbol table
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row.Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                            {
                                try
                                {
                                    if (Convert.ToString(row.Cells[1].Value) == "String Delimiter")
                                    {
                                        break;
                                    }
                                    arithmetic.Push(row2.Cells[1].Value);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                                break;
                            }

                        }
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Extra Keyword") //next token
                    {
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Numbr Literal" || Convert.ToString(row.Cells[1].Value) == "Numbar Literal" || Convert.ToString(row.Cells[1].Value) == "Troof Literal")
                    {
                        try
                        {
                            arithmetic.Push(row.Cells[0].Value); //push in stack
                            varCount += 1;
                        }
                        catch (System.FormatException e)
                        {
                            return;
                        }
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "String Delimiter" && delimiter == 0)
                    {
                        stringFlag = 1;
                        continue;
                    }
                    else if (stringFlag == 1)
                    {
                        arithmetic.Push("\"" + row.Cells[0].Value + "\"");
                        delimiter = 1;
                        varCount += 1;
                        stringFlag = 0;
                        continue;
                    }
                    else if (delimiter == 1)
                    {
                        delimiter = 0;
                    }
                    else
                    {
                        varCount = 0;
                        //push the operations
                        if (Convert.ToString(row.Cells[1].Value) == "Addition Arithmetic Operator")
                        {
                            arithmetic.Push("+");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Subtraction Arithmetic Operator")
                        {
                            arithmetic.Push("-");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Multiplication Arithmetic Operator")
                        {
                            arithmetic.Push("*");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Division Arithmetic Operator")
                        {
                            arithmetic.Push("/");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Modulo Operator")
                        {
                            arithmetic.Push("%");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Greater than Comparison Operator")
                        {
                            arithmetic.Push(">");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Less than Comparison Operator")
                        {
                            arithmetic.Push("<");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Comparison Operator")
                        {
                            arithmetic.Push("diffrint");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "Equality Comparison Operator")
                        {
                            arithmetic.Push("bothsaem");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "AND Operator")
                        {
                            arithmetic.Push("and");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "OR Operator")
                        {
                            arithmetic.Push("or");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "XOR Operator")
                        {
                            arithmetic.Push("xor");
                            operationFlag = 1;
                            continue;
                        }
                        else if (Convert.ToString(row.Cells[1].Value) == "NOT Operator")
                        {
                            operationFlag = 1;
                            arithmetic.Push("not");
                            continue;
                        }
                    }

                    if (varCount == 2) //if there are 2 variables already
                    {
                        while (arithmetic.Count > 2)
                        {
                            varCount = 0;
                            Object operand2 = new Object();
                            Object operand1 = new Object();
                            string operation = "";
                            operand2 = arithmetic.Pop();
                            
                            operand1 = arithmetic.Pop();
                            if (Convert.ToString(operand1) != "WIN" && Convert.ToString(operand1) != "FAIL")
                            {
                                try
                                {
                                    Convert.ToString(Convert.ToDouble((Convert.ToString(operand1)).Trim('"')));
                                }
                                catch (Exception e)
                                {
                                    varCount = 1;
                                    arithmetic.Push(operand1);
                                    arithmetic.Push(operand2);
                                    break;
                                }
                            }
                            operation = Convert.ToString(arithmetic.Pop());

                            Object ans = 0;
                            Object comp = "";

                            Console.Write(operand2);
                            Console.Write(operand1);
                            Console.Write(operation + "\n");

                            var newRow = this.dataGridView2.Rows[rowIndex];
                            if (operation == "+")
                            {
                                try
                                {
                                    ans = Convert.ToString(Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) + Convert.ToDouble((Convert.ToString(operand2)).Trim('"')));
                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == "-")
                            {
                                try
                                {

                                    ans = Convert.ToString(Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) - Convert.ToDouble((Convert.ToString(operand2)).Trim('"')));
                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == "*")
                            {
                                try
                                {
                                    ans = Convert.ToString(Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) * Convert.ToDouble((Convert.ToString(operand2)).Trim('"')));
                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == "/")
                            {
                                try
                                {
                                    ans = Convert.ToString(Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) / Convert.ToDouble((Convert.ToString(operand2)).Trim('"')));
                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == "%")
                            {
                                try
                                {
                                    ans = Convert.ToString(Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) % Convert.ToDouble((Convert.ToString(operand2)).Trim('"')));

                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == ">")
                            {
                                try
                                {
                                    if (Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) > Convert.ToDouble((Convert.ToString(operand2)).Trim('"')))
                                    {
                                        ans = operand1;
                                    }
                                    else
                                    {
                                        ans = operand2;
                                    }
                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == "<")
                            {
                                try
                                {
                                    if (Convert.ToDouble((Convert.ToString(operand1)).Trim('"')) < Convert.ToDouble((Convert.ToString(operand2)).Trim('"')))
                                    {
                                        ans = operand1;
                                    }
                                    else
                                    {
                                        ans = operand2;
                                    }
                                    arithmetic.Push(ans);
                                    varCount += 1;
                                }
                                catch (System.FormatException e)
                                {
                                    richTextBox2.Text += e;
                                    return;
                                }
                            }
                            else if (operation == "diffrint")
                            {
                                if (operand1.Equals(operand2))
                                {
                                    comp = "FAIL";
                                }
                                else
                                {
                                    comp = "WIN";
                                }

                                arithmetic.Push(comp);
                                varCount += 1;
                                compFlag = 1;
                            }
                            else if (operation == "bothsaem")
                            {
                                if (operand1.Equals(operand2))
                                {
                                    comp = "WIN";
                                }
                                else
                                {
                                    comp = "FAIL";
                                }
                                arithmetic.Push(comp);
                                varCount += 1;
                                compFlag = 1;
                            }
                            else if (operation == "and")
                            {
                                if ((Convert.ToString(operand1) == "WIN") && (Convert.ToString(operand2) == "WIN"))
                                {
                                    comp = "WIN";
                                }
                                else
                                {
                                    comp = "FAIL";
                                }
                                arithmetic.Push(comp);
                                varCount += 1;
                                compFlag = 1;

                            }
                            else if (operation == "or")
                            {
                                if ((Convert.ToString(operand1) == "WIN") || (Convert.ToString(operand2) == "WIN"))
                                {
                                    comp = "WIN";
                                }
                                else
                                {
                                    comp = "FAIL";
                                }
                                arithmetic.Push(comp);
                                varCount += 1;
                                compFlag = 1;
                            }
                            else if (operation == "xor")
                            {
                                if ((Convert.ToString(operand1) == "WIN") || (Convert.ToString(operand2) == "WIN"))
                                {
                                    comp = "FAIL";
                                }
                                else
                                {
                                    comp = "WIN";
                                }
                                arithmetic.Push(comp);
                                varCount += 1;
                                compFlag = 1;
                            }
                            else if (operation == "not")
                            {
                                if (Convert.ToString(operand1) == "WIN")
                                {
                                    operand1 = "FAIL";
                                }
                                else
                                {
                                    operand1 = "WIN";
                                }
                                arithmetic.Push(operand1);
                                arithmetic.Push(operand2);
                                notFlag = 0;
                                compFlag = 1;
                            }
                            else if (operation == "all of")
                            {
                                if ((Convert.ToString(operand1) == "WIN") && (Convert.ToString(operand2) == "WIN"))
                                {
                                    comp = "WIN";
                                }
                                else
                                {
                                    comp = "FAIL";
                                }
                                arithmetic.Push("all of");

                                arithmetic.Push(comp);
                                compFlag = 1;
                                arityFlag = 1;
                                varCount = 1;
                            }
                            else if (operation == "any of")
                            {
                                if ((Convert.ToString(operand1) == "FAIL") && (Convert.ToString(operand2) == "FAIL"))
                                {
                                    comp = "FAIL";
                                }
                                else
                                {
                                    comp = "WIN";
                                }
                                arithmetic.Push("any of");

                                arithmetic.Push(comp);
                                compFlag = 1;
                                arityFlag = 1;
                                varCount = 1;
                            }
                        }

                        if (arithmetic.Count != 1)
                        {
                            continue;
                        }
                        if (arithmetic.Count == 2 && operationFlag == 1)
                        {
                            varCount = 1;
                            operationFlag = 0;
                            continue;
                        }
                        else
                        {
                            
                            foreach (DataGridViewRow row2 in dataGridView2.Rows)
                            {
                                if (Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                                {
                                    dataGridView2.Rows.RemoveAt(row2.Index); // delete existing row
                                    break;
                                }
                            }

                            DataGridViewRow row1 = new DataGridViewRow();
                            row1.CreateCells(dataGridView2);
                            if (computeFlag == 1)
                            {
                                row1.Cells[0].Value = "Computed Exp";
                            }
                            else if (itFlag == 1)
                            {
                                itFlag = 0;
                                if (arityFlag == 1)
                                {
                                    continue;
                                }
                                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                                {
                                    if (Convert.ToString(row2.Cells[0].Value) == "IT") //if value of variable is found
                                    {
                                        dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                                        break;
                                    }
                                }
                                row1.Cells[0].Value = "IT";
                            }
                            else
                            {
                                if (arityFlag == 1)
                                {
                                    continue;
                                }
                                
                                row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                            }

                            dataGridView2.Rows.Add(row1);
                            if (compFlag == 1)
                            {
                                row1.Cells[1].Value = Convert.ToString(arithmetic.Pop());
                                compFlag = 0;
                            }
                            else
                            {
                                row1.Cells[1].Value = Convert.ToDouble(arithmetic.Pop());
                            }
                            if (computeFlag == 1)
                            {
                                computeFlag = 2;
                            }

                            assignComputeFlag = 0;
                            varCount = 0;
                            arithmeticFlag = 0;
                            varFlag = 0;
                            initialized = 0;
                            ihasaFlag = 0;
                            varFlag2 = 0;
                            ihasaCount = 0;
                            currVarIndex = 0;
                        }
                    }
                    if (arithmetic.Count == 2)
                    {
                        if (notFlag == 1)
                        {

                            foreach (DataGridViewRow row2 in dataGridView2.Rows)
                            {

                                if (Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                                {
                                    dataGridView2.Rows.RemoveAt(row2.Index); // delete existing row
                                    break;
                                }
                            }

                            Object operand1 = arithmetic.Pop();
                            string operation = Convert.ToString(arithmetic.Pop());
                            if (operation == "not")
                            {
                                if (Convert.ToString(operand1) == "WIN")
                                {
                                    operand1 = "FAIL";
                                }
                                else
                                {
                                    operand1 = "WIN";
                                }
                                arithmetic.Push(operand1);
                                notFlag = 0;
                                DataGridViewRow row1 = new DataGridViewRow();
                                row1.CreateCells(dataGridView2);
                                if (itFlag == 1)
                                {
                                    itFlag = 0;
                                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                                    {
                                        if (Convert.ToString(row2.Cells[0].Value) == "IT") //if value of variable is found
                                        {
                                            dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                                            break;
                                        }
                                    }
                                    row1.Cells[0].Value = "IT";
                                }
                                else
                                {
                                    row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                                }
                                dataGridView2.Rows.Add(row1);
                                row1.Cells[1].Value = Convert.ToString(arithmetic.Pop());
                                varCount = 0;
                                arithmeticFlag = 0;
                                varFlag = 0;
                            }
                        }
                    }
                    if (computeFlag != 2)
                    {
                        continue;
                    }

                }
                if (concatenateFlag == 1) //if smoosh is found
                {
                    if (Convert.ToString(row.Cells[1].Value) == "Variable" && nextString == 1) //if value to be concatenated is a variable
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            
                            if (Convert.ToString(row.Cells[0].Value) == Convert.ToString(row2.Cells[0].Value)) //if value of variable is found
                            {
                                concatenation.Push(Convert.ToString(row2.Cells[1].Value).Trim('"')); //value from symbol table
                            }
                        }
                        nextString = 0;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Yarn Literal" && nextString == 1)
                    {
                        concatenation.Push(Convert.ToString(row.Cells[0].Value).Trim('"')); //value from lexemes table
                        nextString = 0;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Extra Keyword") //AN
                    {
                        nextString = 1;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "String Delimiter")
                    {
                        continue;
                    }
                    else if ((Convert.ToString(row.Cells[1].Value) == "Numbr Literal" || Convert.ToString(row.Cells[1].Value) == "Numbar Literal" || Convert.ToString(row.Cells[1].Value) == "Troof Literal") && nextString == 1)
                    {
                        nextString = 0;
                        concatenation.Push(Convert.ToString(row.Cells[0].Value));
                        continue;
                    }
                    else //if finished
                    {
                        String smooshResult = "";
                        Object toAppend = new Object();
                        while (concatenation.Count != 0)
                        {
                            toAppend = Convert.ToString(concatenation.Pop());
                            smooshResult = Convert.ToString(toAppend) + smooshResult; //add to front
                        }
                        DataGridViewRow row1 = new DataGridViewRow();
                        row1.CreateCells(dataGridView2);
                        if (itFlag == 1)
                        {
                            foreach (DataGridViewRow row2 in dataGridView2.Rows)
                            {
                                if (Convert.ToString(row2.Cells[0].Value) == "IT") //if value of variable is found
                                {
                                    dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                                    break;
                                }
                            }
                            row1.Cells[0].Value = "IT";
                        }
                        else
                        {
                            
                            foreach (DataGridViewRow row2 in dataGridView2.Rows)
                            {
                                if (Convert.ToString(row2.Cells[0].Value) == Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value)) //if value of variable is found
                                {
                                    dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                                    break;
                                }
                            }
                            row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                        }

                        row1.Cells[1].Value = smooshResult;
                        dataGridView2.Rows.Add(row1);
                        smooshDone = 1;
                        if (outputFlag == 0)
                        {
                            concatenateFlag = 0;
                        }
                    }
                }

                if (outputFlag == 1) //if there is something to be printed
                {
                    if (Convert.ToString(row.Cells[1].Value) == "Variable" && computeFlag == 0) //if value to be printed is a variable
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row.Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                            {
                                richTextBox2.Text += row2.Cells[1].Value + "\n";
                                outputFlag = 0;
                                concatenateFlag = 0;
                            }
                        }
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "String Delimiter" && delimiter == 0) //if value to be printed is a string
                    {
                        stringFlag = 1;
                        continue;
                    }
                    else if (stringFlag == 1)
                    {
                        stringDelimiter = 1;
                        stringFlag = 0;
                        richTextBox2.Text += row.Cells[0].Value + "\n";
                        continue;
                    }
                    else if (stringDelimiter == 1)
                    {
                        stringDelimiter = 0;
                        outputFlag = 0;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Numbr Literal" && computeFlag == 0)
                    {
                        richTextBox2.Text += row.Cells[0].Value + "\n";
                        outputFlag = 0;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "Numbar Literal" && computeFlag == 0)
                    {
                        richTextBox2.Text += row.Cells[0].Value + "\n";
                        outputFlag = 0;
                        continue;
                    }
                    else if (computeFlag == 2)
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row2.Cells[0].Value) == "Computed Exp") //VISIBLE IT
                            {
                                richTextBox2.Text += row2.Cells[1].Value + "\n";
                                outputFlag = 0;
                                dataGridView2.Rows.RemoveAt(row2.Index);

                            }
                        }
                        outputFlag = 0;
                        computeFlag = 0;
                        itFlag = 0;
                        continue;
                    }
                    else if (concatenateFlag == 1 && smooshDone == 1 && itFlag == 1)
                    {
                        concatenateFlag = 0;
                        smooshDone = 0;
                        itFlag = 0;
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row2.Cells[0].Value) == "IT") //VISIBLE IT
                            {
                                richTextBox2.Text += row2.Cells[1].Value + "\n";
                                dataGridView2.Rows.RemoveAt(row2.Index);
                            }
                        }
                        outputFlag = 0;
                        continue;
                    }
                    else if (Convert.ToString(row.Cells[1].Value) == "IT") //if value to be printed is a string
                    {
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (Convert.ToString(row2.Cells[0].Value) == "IT") //VISIBLE IT
                            {
                                richTextBox2.Text += row2.Cells[1].Value + "\n";
                                outputFlag = 0;
                                itFlag = 0;
                            }
                        }
                        continue;
                    }
                }

                if (inputFlag == 1 && Convert.ToString(row.Cells[1].Value) == "Variable") //GIMMEH <variable>
                {
                    String promptValue = "";
                    inputFlag = 0;

                    stop = true;
                    currVarIndex = row.Index; //update index

                    while (stop == true)
                    {
                        promptValue = this.ShowInputPrompt(dataGridView1.Rows[currVarIndex].Cells[0].Value.ToString()); //this is the value of the variable from the input from gimmeh
                        stop = false; //stop the loop
                    }

                    foreach (DataGridViewRow row2 in dataGridView2.Rows) //for each row in datagrid2
                    {
                        if (Convert.ToString(dataGridView1.Rows[currVarIndex].Cells[0].Value) == Convert.ToString(row2.Cells[0].Value))
                        {
                            noErrFlag = 1;
                            dataGridView2.Rows.RemoveAt(row2.Index); //delete existing row
                            break;
                        }
                    }

                    if (noErrFlag == 0)
                    {
                        richTextBox2.Text += "Error. Undeclared variable.";
                        return;
                    }

                    DataGridViewRow row1 = new DataGridViewRow();
                    row1.CreateCells(dataGridView2);
                    row1.Cells[0].Value = dataGridView1.Rows[currVarIndex].Cells[0].Value;
                    row1.Cells[1].Value = Convert.ToString("\"" + promptValue + "\"");
                    dataGridView2.Rows.Add(row1);
                    currVarIndex = 0;
                }
            }
        }

        private void execute_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView2.DataSource = null;
            this.dataGridView2.Rows.Clear();
            this.richTextBox2.Clear();
            readCode();
            readLexemesTable();
        }

        private String ShowInputPrompt(String var)
        {
            Form inputPrompt = new Form();
            inputPrompt.Width = 300;
            inputPrompt.Height = 100;
            TextBox inputBox = new TextBox() { Width = 200 };
            Button confirmation = new Button() { Text = "Enter " + var, Top = 30, Width = 100 };
            confirmation.Click += (sender, e) => { inputPrompt.Close(); };
            inputPrompt.Controls.Add(confirmation);
            inputPrompt.Controls.Add(inputBox);
            inputPrompt.ShowDialog();
            return inputBox.Text.ToString();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
        }
    }
}
