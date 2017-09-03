using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceRandomizer.Utils.CmdLine
{
    class CmdArg    //Currently deeply flawed as i need to have the list hold refs of the original (static) object
    {
        #region Constructor
        public CmdArg(string shortCmd, string longCmd = "", string description = "", int addArgsCnt = 0)
        {
            _shortCmd = shortCmd;
            _longCmd = longCmd;
            _description = description;
            _addArgsCnt = addArgsCnt;
            additionArgsList = new string[addArgsCnt];    //removes 1 il instruction using parameter
            CmdParser.AddCmdArg(this);
        }
        #endregion

        #region Getters
        public int GetAdditionalArgCount() { return _addArgsCnt; }
        public string GetShortCommand() { return _shortCmd; }
        public string GetLongCommand() { return _longCmd; }
        public string GetDescription() { return _description; }
        public string GetAdditionalArg(int idx) { return (idx >= _addArgsCnt) ? "" : additionArgsList[idx]; }
        public bool IsParsed() { return WasParsed; }
        #endregion

        #region Setters
        public void SetAdditionalArg(int idx, string str) { if (idx < _addArgsCnt) additionArgsList[idx] = str; }
        public void SetFinishedParsingCllbk(Func<CmdArg, bool> action) { ArgsParsedAction = action; }
        #endregion

        #region Virtuals
        public virtual bool ParseArg(ref List<string> argList)
        {
            bool ParsingStarted = false;
            int addArgs = _addArgsCnt;
            for(int i = 0; i < argList.Count; ++i)
            {
                if(argList[i].ToLower() == _shortCmd || argList[i].ToLower() == _longCmd)
                {
                    argList.RemoveAt(i);
                    i--;
                    ParsingStarted = true;
                    WasParsed = true;
                    continue;
                }

                if(ParsingStarted)
                {
                    if(addArgs > 0)
                    {
                        additionArgsList[_addArgsCnt - addArgs] = argList[i];
                        addArgs--;
                        argList.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ParsingStarted)
            {
                if (addArgs > 0)    //didnt fill all fields
                    return false;
                return ArgsParsedAction(this);
            }
            else
                return true;
        }
        #endregion

        #region Variables
        private Func<CmdArg,bool> ArgsParsedAction = (cmd) => { return true; };
        private string[] additionArgsList;
        private bool WasParsed = false;
        private string _shortCmd, _longCmd, _description;
        private int _addArgsCnt;
        #endregion
    }
}
