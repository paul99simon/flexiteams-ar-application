using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Exceptions;
using System;
using System.Security.Cryptography.X509Certificates;

namespace FlexiTeams.Util;

public class ISO8601
{

    public int Years;
    public int Months;
    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    public ISO8601(string iso8601)
    {
        var ptstrings =  iso8601.Split('T');

        if (ptstrings.Length == 2 ) {
            GetPString(ptstrings[0]);
            GetTString(ptstrings[1]);
        }
        else
        {
            GetPString(ptstrings[0]);
        }

        void GetPString(string pString){
            pString = pString.Substring(1);
            
            var temp = pString.Split('Y');
            if(temp.Length == 2 )
            {
                Years = int.Parse(temp[0]);
                temp = temp[1].Split("M");
            }
            else
            {
                temp = temp[0].Split("M");
            }

            if (temp.Length == 2)
            {
                Months = int.Parse(temp[0]);
                temp = temp[1].Split("D");
            } else
            {
                temp = temp[0].Split("D");
            }

            if (temp.Length == 2)
            {
                Days = int.Parse(temp[0]);
            }
        }

        void GetTString(string tString){
            
            var temp = tString.Split('H');
            if( temp.Length == 2 )
            {
                Hours = int.Parse(temp[0]);
                temp = temp[1].Split("M");
            }
            else
            {
                 temp = temp[0].Split("M");
            }

            if (temp.Length == 2)
            {
                Minutes = int.Parse(temp[0]);
                temp = temp[1].Split("S");
            } else
            {
                temp = temp[0].Split("S");
            }

            if(temp.Length == 2)
            {
                Seconds = int.Parse(temp[0]);
            }
        }
    }


    public static string ToXml(int years, int months, int days,int hours, int minutes, int seconds)
    {

        if (years <= 0 & months <= 0 & days <= 0 & hours <= 0 & minutes <= 0 & seconds <= 0) throw new ISO8601Exception("All parameters less equal '0'");

        string result = "P";

        if (years > 0)
        {
            result += years + "Y";
        }

        if (months > 0)
        {
            result += months + "M";
        }

        if (days > 0)
        {
            result += days + "D";
        }

        if(hours > 0 || minutes > 0 ||seconds > 0) {

            result += "T";

            if(hours > 0)
            {
                result += hours + "H";
            }

            if(minutes > 0)
            {
                result += minutes + "M";
            }

            if(seconds > 0)
            {
                result += seconds + "S";
            }
        }

        return result;
    }
}
