// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace CPWizard
{
	public class StringTools
	{
		private static Color[] ColorArray = new Color[]
    {
            Color.AliceBlue,
			Color.AntiqueWhite,
			Color.Aqua,
			Color.Aquamarine,
			Color.Azure,
			Color.Beige,
			Color.Bisque,
			Color.Black,
			Color.BlanchedAlmond,
			Color.Blue,
			Color.BlueViolet,
			Color.Brown,
			Color.BurlyWood,
			Color.CadetBlue,
			Color.Chartreuse,
			Color.Chocolate,
			Color.Coral,
			Color.CornflowerBlue,
			Color.Cornsilk,
			Color.Crimson,
			Color.Cyan,
			Color.DarkBlue,
			Color.DarkCyan,
			Color.DarkGoldenrod,
			Color.DarkGray,
			Color.DarkGreen,
			Color.DarkKhaki,
			Color.DarkMagenta,
			Color.DarkOliveGreen,
			Color.DarkOrange,
			Color.DarkOrchid,
			Color.DarkRed,
			Color.DarkSalmon,
			Color.DarkSeaGreen,
			Color.DarkSlateBlue,
			Color.DarkSlateGray,
			Color.DarkTurquoise,
			Color.DarkViolet,
			Color.DeepPink,
			Color.DeepSkyBlue,
			Color.DimGray,
			Color.DodgerBlue,
			Color.Firebrick,
			Color.FloralWhite,
			Color.ForestGreen,
			Color.Fuchsia,
			Color.Gainsboro,
			Color.GhostWhite,
			Color.Gold,
			Color.Goldenrod,
			Color.Gray,
			Color.Green,
			Color.GreenYellow,
			Color.Honeydew,
			Color.HotPink,
			Color.IndianRed,
			Color.Indigo,
			Color.Ivory,
			Color.Khaki,
			Color.Lavender,
			Color.LavenderBlush,
			Color.LawnGreen,
			Color.LemonChiffon,
			Color.LightBlue,
			Color.LightCoral,
			Color.LightCyan,
			Color.LightGoldenrodYellow,
			Color.LightGray,
			Color.LightGreen,
			Color.LightPink,
			Color.LightSalmon,
			Color.LightSeaGreen,
			Color.LightSkyBlue,
			Color.LightSlateGray,
			Color.LightSteelBlue,
			Color.LightYellow,
			Color.Lime,
			Color.LimeGreen,
			Color.Linen,
			Color.Magenta,
			Color.Maroon,
			Color.MediumAquamarine,
			Color.MediumBlue,
			Color.MediumOrchid,
			Color.MediumPurple,
			Color.MediumSeaGreen,
			Color.MediumSlateBlue,
			Color.MediumSpringGreen,
			Color.MediumTurquoise,
			Color.MediumVioletRed,
			Color.MidnightBlue,
			Color.MintCream,
			Color.MistyRose,
			Color.Moccasin,
			Color.NavajoWhite,
			Color.Navy,
			Color.OldLace,
			Color.Olive,
			Color.OliveDrab,
			Color.Orange,
			Color.OrangeRed,
			Color.Orchid,
			Color.PaleGoldenrod,
			Color.PaleGreen,
			Color.PaleTurquoise,
			Color.PaleVioletRed,
			Color.PapayaWhip,
			Color.PeachPuff,
			Color.Peru,
			Color.Pink,
			Color.Plum,
			Color.PowderBlue,
			Color.Purple,
			Color.Red,
			Color.RosyBrown,
			Color.RoyalBlue,
			Color.SaddleBrown,
			Color.Salmon,
			Color.SandyBrown,
			Color.SeaGreen,
			Color.SeaShell,
			Color.Sienna,
			Color.Silver,
			Color.SkyBlue,
			Color.SlateBlue,
			Color.SlateGray,
			Color.Snow,
			Color.SpringGreen,
			Color.SteelBlue,
			Color.Tan,
			Color.Teal,
			Color.Thistle,
			Color.Tomato,
			Color.Transparent,
			Color.Turquoise,
			Color.Violet,
			Color.Wheat,
			Color.White,
			Color.WhiteSmoke,
			Color.Yellow,
			Color.YellowGreen
			};

		private static Regex m_removeVersionRegex = new Regex(@"(\((v|ver|rev)\s?\d([a-z\d.]+)\))|(?<=\s)(v|ver|rev)\s?\d([a-z\d.]+)", RegexOptions.Compiled);

		public static string RemoveVersionNumber(string text)
		{
			return RemoveSquareBrackets(m_removeVersionRegex.Replace(text, String.Empty));
		}

		private static Regex m_removeSquareBracketsRegex = new Regex(@"\[.+?\]+", RegexOptions.Compiled);

		public static string RemoveSquareBrackets(string text)
		{
			text = m_removeSquareBracketsRegex.Replace(text, String.Empty);

			return text.Trim();
		}

		private static Regex m_removeRoundBracketsRegex = new Regex(@"\(.+?\)+", RegexOptions.Compiled);

		public static string RemoveRoundBrackets(string text)
		{
			text = m_removeRoundBracketsRegex.Replace(text, String.Empty);

			return text.Trim();
		}

		private static Regex m_removeBracketsRegex = new Regex(@"(\(|\[|\{).+?(\)|\]|\})+", RegexOptions.Compiled);

		public static string RemoveBrackets(string text)
		{
			text = m_removeBracketsRegex.Replace(text, String.Empty);
			return text.Trim();
		}

		public static ulong FromHexToLong(string hexString)
		{
			if (String.IsNullOrEmpty(hexString))
				return 0;

			return UInt64.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
		}

		public static bool TryConvertToString<T>(T value, out string valueString)
		{
			valueString = null;

			try
			{
				TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
				valueString = typeConverter.ConvertToString(null, CultureInfo.CreateSpecificCulture("en-US"), value);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static bool TryConvertFromString<T>(string valueString, out T value)
		{
			value = default(T);

			if (String.IsNullOrEmpty(valueString))
				return false;

			try
			{
				if (typeof(T) == typeof(bool))
				{
					if (valueString.ToLower().Equals("yes"))
					{
						value = (T)System.Convert.ChangeType(true, typeof(T));

						return true;
					}
					else if (valueString.ToLower().Equals("no"))
					{
						value = (T)System.Convert.ChangeType(false, typeof(T));

						return true;
					}
					else if (valueString.ToLower().Equals("1"))
					{
						value = (T)System.Convert.ChangeType(true, typeof(T));

						return true;
					}
					else if (valueString.ToLower().Equals("0"))
					{
						value = (T)System.Convert.ChangeType(false, typeof(T));

						return true;
					}
				}

				TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
				value = (T)typeConverter.ConvertFromString(null, CultureInfo.CreateSpecificCulture("en-US"), valueString);
			}
			catch
			{
				return false;
			}

			return true;
		}

		public static T FromString<T>(string valueString)
		{
			T value;

			TryConvertFromString(valueString, out value);

			return value;
		}

		public static T FromString<T>(string valueString, T defaultValue)
		{
			T value;

			if (!TryConvertFromString(valueString, out value))
				return defaultValue;

			return value;
		}

		public static string ToString<T>(T value)
		{
			string valueString = null;

			TryConvertToString(value, out valueString);

			return valueString;
		}

		public static string ArrayToString<T>(T[] valueArray, string delimeter)
		{
			if (valueArray == null)
				return null;

			string[] stringArray = new string[valueArray.Length];

			for (int i = 0; i < valueArray.Length; i++)
				TryConvertToString(valueArray[i], out stringArray[i]);

			return String.Join(delimeter, stringArray);
		}

		public static T[] StringToArray<T>(string valueString, string delimeter)
		{
			string[] dataArray = valueString.Split(new char[] { '~' });
			T[] valueArray = new T[dataArray.Length];
			TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));

			for (int i = 0; i < valueArray.Length; i++)
				TryConvertFromString(dataArray[i], out valueArray[i]);

			return valueArray;
		}

		public static T[] Array<T>(T value, int count)
		{
			T[] result = new T[count];
			for (int i = 0; i < count; i++)
				result[i] = value;

			return result;
		}

		public static string SubString(string str, int length)
		{
			if (String.IsNullOrEmpty(str))
				return str;

			if (str.Length < length)
				return str;
			else
				return str.Substring(0, length);
		}

		public static byte[] StrToByteArray(string str)
		{
			return System.Text.Encoding.Default.GetBytes(str);
		}

		public static string ByteArrayToStr(byte[] bytes)
		{
			return System.Text.Encoding.Default.GetString(bytes);
		}

		public static string ByteToStr(byte b)
		{
			return System.Text.Encoding.Default.GetString(new byte[] { b });
		}

		public static bool IsFloat(string val)
		{
			if (Regex.IsMatch(val.ToString(), @"^[+-]?([0-9]*\.?[0-9]+|[0-9]+\.?[0-9]*)([eE][+-]?[0-9]+)?$"))
				return true;
			else return false;
		}

		public static bool IsAlpha(string str)
		{
			Regex regex = new Regex("[^a-zA-Z]");

			return !regex.IsMatch(str);
		}

		public static bool IsAlphaNumeric(string str)
		{
			Regex regex = new Regex("[^a-zA-Z0-9]");

			return !regex.IsMatch(str);
		}

		public static string RemoveDigits(string key)
		{
			return Regex.Replace(key, @"\d", "");
		}

		public static string RemoveAlpha(string str)
		{
			if (str == null)
				return null;

			StringBuilder sb = new StringBuilder();

			foreach (char c in str)
				if (!Char.IsLetter(c))
					sb.Append(c);

			return sb.ToString();
		}

		public static string ReplaceFirst(string str, string oldValue, string newValue)
		{
			string[] strSplit = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < strSplit.Length; i++)
			{
				if (strSplit[i] == oldValue)
				{
					strSplit[i] = newValue;
					return String.Join(" ", strSplit);
				}
			}

			return str;
		}

		public static bool StrContainsOnlyChar(string str, char c)
		{
			if (String.IsNullOrEmpty(str))
				return false;

			for (int i = 0; i < str.Length; i++)
				if (str[i] != c)
					return false;

			return true;
		}

		public static string StrFillChar(char c, int count)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < count; i++)
				sb.Append(c);

			return sb.ToString();
		}

		public static string[] SplitString(string str, string[] splitStr)
		{
			int strPos = 0;
			List<string> strArray = new List<string>();

			string[] strSplit = str.Split(splitStr, StringSplitOptions.None);

			for (int i = 0; i < strSplit.Length; i++)
			{
				if (strSplit[i] != String.Empty)
					strArray.Add(strSplit[i]);

				strPos += strSplit[i].Length;

				for (int j = 0; j < splitStr.Length; j++)
				{
					if (str.IndexOf(splitStr[j], strPos) == strPos)
					{
						if (splitStr[j] != String.Empty)
							strArray.Add(splitStr[j]);

						strPos += splitStr[j].Length;

						break;
					}
				}
			}

			return strArray.ToArray();
		}

		public static bool IsVariable(string text)
		{
			if (String.IsNullOrEmpty(text))
				return false;

			if (text.Contains("|"))
			{
				string[] variableArray = text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < variableArray.Length; i++)
				{
					if (String.IsNullOrEmpty(variableArray[i]))
						continue;

					return (variableArray[i].StartsWith("[") && variableArray[i].EndsWith("]"));
				}
			}
			
			return (text.StartsWith("[") && text.EndsWith("]"));
		}

		public static bool TryGetVariableName(string text, out string variableName)
		{
			variableName = text;

			if (String.IsNullOrEmpty(text))
				return false;

			if (!text.StartsWith("[") || !text.EndsWith("]"))
				return false;

			variableName = text.Substring(1, text.Length - 2);

			return true;
		}

		public static string GetVariableName(string text)
		{
			string variableName = text;

			TryGetVariableName(text, out variableName);

			return variableName;
		}

		public static string FixVariables(string text)
		{
			if (String.IsNullOrEmpty(text))
				return text;

			if (text.Contains("|"))
			{
				string[] variableArray = text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				List<string> variableList = new List<string>();

				for (int i = 0; i < variableArray.Length; i++)
				{
					if (String.IsNullOrEmpty(variableArray[i]))
						continue;

					if (variableArray[i].StartsWith("[") && variableArray[i].EndsWith("]"))
					{
						variableList.Add(variableArray[i]);

						continue; 
					}

					variableList.Add(String.Format("[{0}]", variableArray[i]));
				}

				return String.Join("|", variableList.ToArray());
			}

			if (text.StartsWith("[") && text.EndsWith("]"))
				return text;

			return String.Format("[{0}]", text);
		}

		public static bool IsInputCode(string text)
		{
			if (String.IsNullOrEmpty(text))
				return false;

			TryGetVariableName(text, out text);

			string[] inputCodeArray = { "PLAYER_", "KEYCODE_", "JOYCODE_", "MOUSECODE_", "MISC_", "EMU_", "GROUP_" };
			  
			foreach (string inputCode in inputCodeArray)
			{
				if(text.StartsWith(inputCode))
					return true;
			}

			return false;
		}

		public static bool ContainsString(string source, string value, bool ignoreCase)
		{
			if (String.IsNullOrEmpty(source))
				return false;

			return (CultureInfo.CreateSpecificCulture("en-US").CompareInfo.IndexOf(source, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None) >= 0);
		}
	}
}