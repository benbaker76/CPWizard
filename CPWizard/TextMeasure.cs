// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CPWizard
{
	public class TextMeasure
	{
		private Font m_font = null;
		private SizeF[] m_charSizes = null;

		public TextMeasure(Font font)
		{
			m_font = font;
			m_charSizes = new SizeF[256];

			/* using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
			{
				for (int i = 0; i < 256; i++)
				{
					string sChar = new String(new char[] { (char)i });

					m_charSizes[i] = System.Windows.Forms.TextRenderer.MeasureText(g, sChar, font, Size.Empty, TextFormatFlags.NoPadding);
				}
			} */

			StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
			RectangleF stringRect = RectangleF.Empty;
			CharacterRange[] characterRangeArray = { new CharacterRange(0, 1) };
			Region[] regionArray = null;

			stringFormat.SetMeasurableCharacterRanges(characterRangeArray);
			stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoClip;

			using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
			{
				for (int i = 0; i < 256; i++)
				{
					string text = new String(new char[] { (char)i });

					regionArray = g.MeasureCharacterRanges(text, m_font, stringRect, stringFormat);

					m_charSizes[i] = regionArray[0].GetBounds(g).Size;
				}
			}
		}

		public SizeF MeasureString(string text)
		{
			SizeF textSize = SizeF.Empty;

			for (int i = 0; i < text.Length; i++)
			{
				SizeF charSize = m_charSizes[(int)text[i]];

				textSize.Width += charSize.Width;

				if (charSize.Height > textSize.Height)
					textSize.Height = charSize.Height;
			}

			return textSize.ToSize();
		}

		/* public Size MeasureString(string text)
		{
			if (String.IsNullOrEmpty(text))
				return Size.Empty;

			StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
			RectangleF stringRect = RectangleF.Empty;
			CharacterRange[] characterRangeArray = { new CharacterRange(0, text.Length) };
			Region[] regionArray = null;

			stringFormat.SetMeasurableCharacterRanges(characterRangeArray);
			stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoClip;

			using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
			{
				regionArray = g.MeasureCharacterRanges(text, m_font, stringRect, stringFormat);
				stringRect = regionArray[0].GetBounds(g);
			}

			return stringRect.Size.ToSize();
		} */
	}
}
