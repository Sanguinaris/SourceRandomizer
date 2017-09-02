using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceRandomizer.Utils.cmdLine
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
            additionArgsList = new List<string>(addArgsCnt);    //removes 1 il instruction using parameter
            CmdParser.AddCmdArg(this);
        }
        #endregion

        #region Getters
        int GetAdditionalArgCount() { return _addArgsCnt; }
        string GetShortCommand() { return _shortCmd; }
        string GetLongCommand() { return _longCmd; }
        string GetDescription() { return _description; }
        string GetAdditionalArg(int idx) { return (idx >= additionArgsList.Count) ? "" : additionArgsList[idx]; }
        #endregion

        #region Setters
        void SetAdditionalArg(int idx, string str) { if (idx < _addArgsCnt) additionArgsList[idx] = str; }
        void SetFinishedParsingCllbk(Func<bool> action) { ArgsParsedAction = action; }
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

            if (addArgs > 0)    //didnt fill all fields
                return false;

            return ArgsParsedAction(); //TODO this no gud. lambda need this as arg and way 2 get add args
        }
        #endregion

        #region Pubs

        #endregion

        #region Variables
        private Func<bool> ArgsParsedAction = () => { return true; };
        private List<string> additionArgsList;
        private string _shortCmd, _longCmd, _description;
        private int _addArgsCnt;
        #endregion
    }
}
