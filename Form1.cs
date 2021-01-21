using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IslandTool
{
    public partial class Form1 : Form
    {
        private string firstTherion = "";
        private string secondTherion = "";
        private string thirdTherion = "";
        private string fourthTherion = "";
        private bool firstType = false;
        private bool secondType = false;
        private bool thirdType = false;
        private bool fourthType = false;
        private List<int> heroList = new List<int>();
        private List<int> mechaList = new List<int>();
        private List<string> therionList = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (heroBox0.Text != null && heroBox0.Text != "")
            {
                if (int.Parse(heroBox0.Text) > iMax)
                {
                    heroBox0.Text = iMax.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = getTextBoxCount(groupBox2.Controls);
            if (count >= 35)
            {
                MessageBox.Show("最大支持35个兵装");
            }
            TextBox textBox = new TextBox();
            textBox.Text = "0";
            textBox.Name = "mbox" + count;
            textBox.Size = new Size(23, 21);
            textBox.Location = new Point(53 + 80 * (count / 7), 43 + 30 * (count % 7));

            Label label = new Label();
            label.Text = "顺序" + (count + 1);
            label.Name = "mlabel" + count;
            label.AutoSize = true;
            label.Location = new Point(6 + 80 * (count / 7), 46 + 30 * (count % 7));
            groupBox2.Controls.Add(label);
            groupBox2.Controls.Add(textBox);
        }
        private int getTextBoxCount(Control.ControlCollection controlCollection)
        {
            int count = 0;
            foreach (var item in controlCollection)
            {
                if (item is TextBox)
                {
                    count++;
                }
            }
            return count;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (heroBox1.Text != null && heroBox1.Text != "")
            {
                if (int.Parse(heroBox1.Text) > iMax)
                {
                    heroBox1.Text = iMax.ToString();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (heroBox2.Text != null && heroBox2.Text != "")
            {
                if (int.Parse(heroBox2.Text) > iMax)
                {
                    heroBox2.Text = iMax.ToString();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (heroBox3.Text != null && heroBox3.Text != "")
            {
                if (int.Parse(heroBox3.Text) > iMax)
                {
                    heroBox3.Text = iMax.ToString();
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int iMax = 20;
            if (heroBox4.Text != null && heroBox4.Text != "")
            {
                if (int.Parse(heroBox4.Text) > iMax)
                {
                    heroBox4.Text = iMax.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //获取武将配置
            getHeroList();
            //获取圣兽配置
            getTherionList();
            //获取兵装配置
            getMechaList();

            int ownTeam = 0;
            int enemyTeam = 0;
            double nowBlood = 1f;
            bool bOwn = true;
            bool bWhiteTiger = false;
            int dragon = 0;
            int tortoise = 0;
            int redBird = 0;
            int mechaNum = 0;
            bool bNew = true;
            listBox1.Items.Add("模拟开始");
            //50回合
            for (int i = 0; i < 50; i++)
            {
                if (ownTeam > 2)
                {
                    break;
                }
                if (nowBlood <= 0)
                {
                    nowBlood = 1f;
                    enemyTeam++;
                    if (enemyTeam > 3)
                        break;
                }
                listBox1.Items.Add("=================================新回合=================================");
                //我方回合
                if (bOwn)
                {
                    if (bNew)
                    {
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam]);
                        bNew = false;
                    }
                    //计算圣兽掉血
                    if (therionList[ownTeam] == "朱雀·极" && redBird < 3 && !checkBox1.Checked)
                    {
                        nowBlood *= 0.9f;
                        redBird++;
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam], msg: "灼烧第" + redBird + "回合");
                    }
                    else if (therionList[ownTeam] == "白虎·极" && !bWhiteTiger)
                    {
                        nowBlood *= 0.5f;
                        bWhiteTiger = true;
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam], msg: "减少当前血量50%");
                    }
                    else if (therionList[ownTeam] == "白虎" && !bWhiteTiger)
                    {
                        nowBlood *= 1f - 0.25f;
                        bWhiteTiger = true;
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam], msg: "减少当前血量25%");
                    }
                    //计算兵装掉血
                    if (mechaNum < mechaList.Count)
                    {
                        nowBlood -= (double)mechaList[mechaNum] / 100f;
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, mecha: mechaList[mechaNum]);
                        mechaNum++;
                    }
                    else
                    {
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood);
                    }
                }
                else
                {
                    int count = 0;
                    if (therionList[ownTeam].Contains("青龙") && dragon == 0)
                    {
                        //青龙变相无敌1回合
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam], msg: "眩晕1回合");
                        dragon++;
                    }
                    else if (therionList[ownTeam].Contains("玄武") && tortoise == 0)
                    {
                        //玄武变相无敌1回合
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam], msg: "我方无敌1回合");
                        tortoise++;
                    }
                    else if (therionList[ownTeam].Contains("玄武·极") && tortoise < 2)
                    {
                        //玄武·极变相无敌1回合
                        tortoise++;
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, therion: therionList[ownTeam], msg: "我方无敌第" + tortoise + "回合");
                    }
                    else
                    {
                        //计算自爆掉血
                        for (int j = 0 + 5 * ownTeam; j < 5 + 5 * ownTeam; j++)
                        {
                            if (j < heroList.Count)
                            {
                                if (heroList[j] >= 0)
                                {
                                    nowBlood *= (1 - (double)(heroList[j] + 15) / 100f);
                                    count++;
                                }
                            }
                        }
                        writeLog(i + 1, bOwn, enemyTeam, ownTeam, nowBlood, blow: count);
                        ownTeam++;
                        bNew = true;
                    }
                }
                bOwn = !bOwn;
            }
            listBox1.Items.Add("模拟结束：敌方剩余血量：" + string.Format("{0:F}", (1f * (3 - enemyTeam) + nowBlood) * 100) + "%");
        }
        private double simulationAction()
        {
            int ownTeam = 0;
            int enemyTeam = 0;
            double nowBlood = 1f;
            bool bOwn = true;
            bool bWhiteTiger = false;
            int dragon = 0;
            int tortoise = 0;
            int redBird = 0;
            int mechaNum = 0;
            bool bNew = true;
            //50回合
            for (int i = 0; i < 50; i++)
            {
                if (ownTeam > 2)
                {
                    break;
                }
                if (nowBlood <= 0)
                {
                    nowBlood = 1f;
                    enemyTeam++;
                    if (enemyTeam > 3)
                        break;
                }
                //我方回合
                if (bOwn)
                {
                    if (bNew)
                    {
                        bNew = false;
                    }
                    //计算圣兽掉血
                    if (therionList[ownTeam] == "朱雀·极" && redBird < 3 && !checkBox1.Checked)
                    {
                        nowBlood *= 0.9f;
                        redBird++;
                    }
                    else if (therionList[ownTeam] == "白虎·极" && !bWhiteTiger)
                    {
                        nowBlood *= 0.5f;
                        bWhiteTiger = true;
                    }
                    else if (therionList[ownTeam] == "白虎" && !bWhiteTiger)
                    {
                        nowBlood *= 1f - 0.25f;
                        bWhiteTiger = true;
                    }
                    //计算兵装掉血
                    if (mechaNum < mechaList.Count)
                    {
                        nowBlood -= (double)mechaList[mechaNum] / 100f;
                        mechaNum++;
                    }
                }
                else
                {
                    int count = 0;
                    if (therionList[ownTeam].Contains("青龙") && dragon == 0)
                    {
                        //青龙变相无敌1回合
                        dragon++;
                    }
                    else if (therionList[ownTeam].Contains("玄武") && tortoise == 0)
                    {
                        //玄武变相无敌1回合
                        tortoise++;
                    }
                    else if (therionList[ownTeam].Contains("玄武·极") && tortoise < 2)
                    {
                        //玄武·极变相无敌1回合
                        tortoise++;
                    }
                    else
                    {
                        //计算自爆掉血
                        for (int j = 0 + 5 * ownTeam; j < 5 + 5 * ownTeam; j++)
                        {
                            if (j < heroList.Count)
                            {
                                if (heroList[j] >= 0)
                                {
                                    nowBlood *= (1 - (double)(heroList[j] + 15) / 100f);
                                    count++;
                                }
                            }
                        }
                        ownTeam++;
                        bNew = true;
                    }
                }
                bOwn = !bOwn;
            }
            nowBlood = 1f * (3 - enemyTeam) + nowBlood;
            return nowBlood;
        }
        private void writeLog(int round, bool bOwn, int enemyTeam, int ownTeam, double nowBlood, string therion = "", int mecha = 0, int blow = 0, string msg = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("第").Append(round).Append("回合");
            if (bOwn)
                builder.Append("★★★我方回合★★★");
            else
                builder.Append("▲▲▲敌方回合▲▲▲");
            if (!string.IsNullOrEmpty(therion))
                builder.Append("---圣兽：《").Append(therion).Append("》");
            if (mecha > 0)
                builder.Append("---兵装：【").Append(mecha).Append("%】");
            if (blow > 0)
                builder.Append("---自爆：{").Append(blow).Append("}个");
            if (!string.IsNullOrEmpty(msg))
                builder.Append(msg);
            builder.Append("---我方第[").Append(ownTeam + 1).Append("]队，敌方第[").Append(enemyTeam + 1).Append("]队，剩余血量：[").Append(string.Format("{0:F}", nowBlood * 100)).Append("%]");
            listBox1.Items.Add(builder.ToString());
        }
        private void getMechaList()
        {
            mechaList.Clear();
            int count = getTextBoxCount(groupBox2.Controls);
            for (int i = 0; i < count - 1; i++)
            {
                string name = "mbox" + i;
                TextBox textBox = (TextBox)Controls.Find(name, true).First();
                if (!string.IsNullOrEmpty(textBox.Text))
                    mechaList.Add(Int32.Parse(textBox.Text));
                else
                    mechaList.Add(-1);
            }
        }

        private void getTherionList()
        {
            therionList.Clear();
            for (int i = 0; i < 4; i++)
            {
                string name = "therionBox" + i;
                ComboBox comboBox = (ComboBox)Controls.Find(name, true).First();
                string type = "therionCheck" + i;
                CheckBox checkBox = (CheckBox)Controls.Find(type, true).First();
                string therion = comboBox.Text;
                if (checkBox.Checked)
                {
                    therion += "·极";
                }
                if (comboBox.Text != null)
                    therionList.Add(therion);
                else
                    therionList.Add("");
            }
        }

        private void getHeroList()
        {
            heroList.Clear();
            for (int i = 0; i < 18; i++)
            {
                string name = "heroBox" + i;
                TextBox textBox = (TextBox)Controls.Find(name, true).First();
                if (!string.IsNullOrEmpty(textBox.Text))
                    heroList.Add(Int32.Parse(textBox.Text));
                else
                    heroList.Add(-1);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            firstTherion = therionBox0.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            firstType = therionCheck0.Checked;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            secondTherion = therionBox1.Text;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            secondType = therionCheck1.Checked;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            thirdTherion = therionBox2.Text;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            thirdType = therionCheck2.Checked;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            fourthTherion = therionBox3.Text;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            fourthType = therionCheck3.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = getTextBoxCount(groupBox2.Controls);
            TextBox removeTextBox = (TextBox)Controls.Find("mbox" + (count - 1), true).First();
            Label removeLabel = (Label)Controls.Find("mlabel" + (count - 1), true).First();
            groupBox2.Controls.Remove(removeTextBox);
            groupBox2.Controls.Remove(removeLabel);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //获取武将配置
            getHeroList();
            //获取圣兽配置
            getTherionList();
            //获取兵装配置
            getMechaList();
            List<string[]> therionListAll = PermutationAndCombination<string>.GetPermutation(therionList.ToArray());
            List<int[]> mechaListAll = PermutationAndCombination<int>.GetPermutation(mechaList.ToArray());
            List<int[]> heroListAll = PermutationAndCombination<int>.GetPermutation(heroList.ToArray());
        }
    }
}
