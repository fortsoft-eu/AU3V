﻿/**
 * This is open-source software licensed under the terms of the MIT License.
 *
 * Copyright (c) 2022-2023 Petr Červinka - FortSoft <cervinka@fortsoft.eu>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 **
 * Version 2.0.2.0
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FortSoft.Tools {

    /// <summary>
    /// Implements the conversion of Unicode characters and strings to ASCII-only
    /// string. Text is converted character-by-character without considering the
    /// context. The mappings for each non-ASCII character is based on its
    /// pronunciation in the associated language with English notation in mind.
    /// Symbolic characters are converted based on their meaning or appearance.
    /// Unknown characters and some known characters are replaced with an empty
    /// string and removed.
    /// </summary>
    public static class ASCII {

        /// <summary>
        /// Checks if the provided character is an ASCII character.
        /// </summary>
        /// <param name="c">Character to check.</param>
        /// <returns>True if the provided character is an ASCII character.
        /// </returns>
        public static bool IsASCII(char c) => c < '\u0080';

        /// <summary>
        /// Checks if the provided string contains only ASCII characters.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <param name="encoding">Input encoding.</param>
        /// <returns>True if the provided string contains only ASCII characters.
        /// </returns>
        public static bool IsASCII(string str, Encoding encoding) {
            byte[] bytes = Encoding.Convert(encoding, Encoding.Unicode, encoding.GetBytes(str));
            foreach (char c in Encoding.Unicode.GetChars(bytes)) {
                if (!IsASCII(c)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the provided string contains only ASCII characters.
        /// </summary>
        /// <param name="str">String to check. Provided string will be treated as
        /// a Unicode string.</param>
        /// <returns>True if the provided string contains only ASCII characters.
        /// </returns>
        public static bool IsASCII(string str) {
            foreach (char c in str.ToCharArray()) {
                if (!IsASCII(c)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Converts the provided Unicode character to the corresponding ASCII
        /// string according to the pronunciation in the associated language with
        /// English notation in mind. Symbolic characters are converted based on
        /// their meaning or appearance.
        /// </summary>
        /// <param name="c">Character to convert.</param>
        /// <returns>Output string.</returns>
        public static string Convert(char c) {
            if (IsASCII(c)) {
                return c.ToString();
            }
            switch (c) {
                case '\u00C0': // À  [LATIN CAPITAL LETTER A WITH GRAVE]
                case '\u00C1': // Á  [LATIN CAPITAL LETTER A WITH ACUTE]
                case '\u00C2': // Â  [LATIN CAPITAL LETTER A WITH CIRCUMFLEX]
                case '\u00C3': // Ã  [LATIN CAPITAL LETTER A WITH TILDE]
                case '\u00C4': // Ä  [LATIN CAPITAL LETTER A WITH DIAERESIS]
                case '\u00C5': // Å  [LATIN CAPITAL LETTER A WITH RING ABOVE]
                case '\u0100': // Ā  [LATIN CAPITAL LETTER A WITH MACRON]
                case '\u0102': // Ă  [LATIN CAPITAL LETTER A WITH BREVE]
                case '\u0104': // Ą  [LATIN CAPITAL LETTER A WITH OGONEK]
                case '\u01CD': // Ǎ  [LATIN CAPITAL LETTER A WITH CARON]
                case '\u01DE': // Ǟ  [LATIN CAPITAL LETTER A WITH DIAERESIS AND MACRON]
                case '\u01E0': // Ǡ  [LATIN CAPITAL LETTER A WITH DOT ABOVE AND MACRON]
                case '\u01FA': // Ǻ  [LATIN CAPITAL LETTER A WITH RING ABOVE AND ACUTE]
                case '\u0200': // Ȁ  [LATIN CAPITAL LETTER A WITH DOUBLE GRAVE]
                case '\u0202': // Ȃ  [LATIN CAPITAL LETTER A WITH INVERTED BREVE]
                case '\u0226': // Ȧ  [LATIN CAPITAL LETTER A WITH DOT ABOVE]
                case '\u023A': // Ⱥ  [LATIN CAPITAL LETTER A WITH STROKE]
                case '\u1D00': // ᴀ  [LATIN LETTER SMALL CAPITAL A]
                case '\u1E00': // Ḁ  [LATIN CAPITAL LETTER A WITH RING BELOW]
                case '\u1EA0': // Ạ  [LATIN CAPITAL LETTER A WITH DOT BELOW]
                case '\u1EA2': // Ả  [LATIN CAPITAL LETTER A WITH HOOK ABOVE]
                case '\u1EA4': // Ấ  [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND ACUTE]
                case '\u1EA6': // Ầ  [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND GRAVE]
                case '\u1EA8': // Ẩ  [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE]
                case '\u1EAA': // Ẫ  [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND TILDE]
                case '\u1EAC': // Ậ  [LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND DOT BELOW]
                case '\u1EAE': // Ắ  [LATIN CAPITAL LETTER A WITH BREVE AND ACUTE]
                case '\u1EB0': // Ằ  [LATIN CAPITAL LETTER A WITH BREVE AND GRAVE]
                case '\u1EB2': // Ẳ  [LATIN CAPITAL LETTER A WITH BREVE AND HOOK ABOVE]
                case '\u1EB4': // Ẵ  [LATIN CAPITAL LETTER A WITH BREVE AND TILDE]
                case '\u1EB6': // Ặ  [LATIN CAPITAL LETTER A WITH BREVE AND DOT BELOW]
                case '\u24B6': // Ⓐ  [CIRCLED LATIN CAPITAL LETTER A]
                case '\uFF21': // Ａ  [FULLWIDTH LATIN CAPITAL LETTER A]
                case '\u0391': // Α  [GREEK CAPITAL LETTER ALPHA]
                case '\u0386': // Ά  [GREEK CAPITAL LETTER ALPHA WITH TONOS]
                case '\u2C80': // Ⲁ  [COPTIC CAPITAL LETTER ALFA]
                case '\u2C6D': // Ɑ  [LATIN CAPITAL LETTER ALPHA]
                case '\u0410': // А  [CYRILLIC CAPITAL LETTER A]
                case '\u2C00': // Ⰰ  [GLAGOLITIC CAPITAL LETTER AZU]
                case '\u2C2D': // Ⱝ  [GLAGOLITIC CAPITAL LETTER TROKUTASTI A]
                case '\u16A8': // ᚨ  [RUNIC LETTER ANSUZ A]
                case '\u16AA': // ᚪ  [RUNIC LETTER AC A]
                case '\u16AB': // ᚫ  [RUNIC LETTER AESC]
                case '\u16AC': // ᚬ  [RUNIC LETTER LONG-BRANCH-OSS O]
                case '\u16AD': // ᚭ  [RUNIC LETTER SHORT-TWIG-OSS O]
                case '\u16C6': // ᛆ  [RUNIC LETTER SHORT-TWIG-AR A]
                case '\u16F8': // ᛸ  [RUNIC LETTER FRANKS CASKET AESC]
                    return "A";
                case '\u00E0': // à  [LATIN SMALL LETTER A WITH GRAVE]
                case '\u00E1': // á  [LATIN SMALL LETTER A WITH ACUTE]
                case '\u00E2': // â  [LATIN SMALL LETTER A WITH CIRCUMFLEX]
                case '\u00E3': // ã  [LATIN SMALL LETTER A WITH TILDE]
                case '\u00E4': // ä  [LATIN SMALL LETTER A WITH DIAERESIS]
                case '\u00E5': // å  [LATIN SMALL LETTER A WITH RING ABOVE]
                case '\u0101': // ā  [LATIN SMALL LETTER A WITH MACRON]
                case '\u0103': // ă  [LATIN SMALL LETTER A WITH BREVE]
                case '\u0105': // ą  [LATIN SMALL LETTER A WITH OGONEK]
                case '\u01CE': // ǎ  [LATIN SMALL LETTER A WITH CARON]
                case '\u01DF': // ǟ  [LATIN SMALL LETTER A WITH DIAERESIS AND MACRON]
                case '\u01E1': // ǡ  [LATIN SMALL LETTER A WITH DOT ABOVE AND MACRON]
                case '\u01FB': // ǻ  [LATIN SMALL LETTER A WITH RING ABOVE AND ACUTE]
                case '\u0201': // ȁ  [LATIN SMALL LETTER A WITH DOUBLE GRAVE]
                case '\u0203': // ȃ  [LATIN SMALL LETTER A WITH INVERTED BREVE]
                case '\u0227': // ȧ  [LATIN SMALL LETTER A WITH DOT ABOVE]
                case '\u0250': // ɐ  [LATIN SMALL LETTER TURNED A]
                case '\u1D8F': // ᶏ  [LATIN SMALL LETTER A WITH RETROFLEX HOOK]
                case '\u1E01': // ḁ  [LATIN SMALL LETTER A WITH RING BELOW]
                case '\u1E9A': // ẚ  [LATIN SMALL LETTER A WITH RIGHT HALF RING]
                case '\u1EA1': // ạ  [LATIN SMALL LETTER A WITH DOT BELOW]
                case '\u1EA3': // ả  [LATIN SMALL LETTER A WITH HOOK ABOVE]
                case '\u1EA5': // ấ  [LATIN SMALL LETTER A WITH CIRCUMFLEX AND ACUTE]
                case '\u1EA7': // ầ  [LATIN SMALL LETTER A WITH CIRCUMFLEX AND GRAVE]
                case '\u1EA9': // ẩ  [LATIN SMALL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE]
                case '\u1EAB': // ẫ  [LATIN SMALL LETTER A WITH CIRCUMFLEX AND TILDE]
                case '\u1EAD': // ậ  [LATIN SMALL LETTER A WITH CIRCUMFLEX AND DOT BELOW]
                case '\u1EAF': // ắ  [LATIN SMALL LETTER A WITH BREVE AND ACUTE]
                case '\u1EB1': // ằ  [LATIN SMALL LETTER A WITH BREVE AND GRAVE]
                case '\u1EB3': // ẳ  [LATIN SMALL LETTER A WITH BREVE AND HOOK ABOVE]
                case '\u1EB5': // ẵ  [LATIN SMALL LETTER A WITH BREVE AND TILDE]
                case '\u1EB7': // ặ  [LATIN SMALL LETTER A WITH BREVE AND DOT BELOW]
                case '\u2090': // ₐ  [LATIN SUBSCRIPT SMALL LETTER A]
                case '\u24D0': // ⓐ  [CIRCLED LATIN SMALL LETTER A]
                case '\u2C65': // ⱥ  [LATIN SMALL LETTER A WITH STROKE]
                case '\u2C6F': // Ɐ  [LATIN CAPITAL LETTER TURNED A]
                case '\uFF41': // ａ  [FULLWIDTH LATIN SMALL LETTER A]
                case '\u00AA': // ª  [FEMININE ORDINAL INDICATOR]
                case '\u03B1': // α  [GREEK SMALL LETTER ALPHA]
                case '\u03AC': // ά  [GREEK SMALL LETTER ALPHA WITH TONOS]
                case '\u2C81': // ⲁ  [COPTIC SMALL LETTER ALFA]
                case '\u0251': // ɑ  [LATIN SMALL LETTER ALPHA]
                case '\u0252': // ɒ  [LATIN SMALL LETTER TURNED ALPHA]
                case '\u1D90': // ᶐ  [LATIN SMALL LETTER ALPHA WITH RETROFLEX HOOK]
                case '\u1D45': // ᵅ  [MODIFIER LETTER SMALL ALPHA]
                case '\u1D9B': // ᶛ  [MODIFIER LETTER SMALL TURNED ALPHA]
                case '\u0430': // а  [CYRILLIC SMALL LETTER A]
                case '\u2C30': // ⰰ  [GLAGOLITIC SMALL LETTER AZU]
                case '\u2C5D': // ⱝ  [GLAGOLITIC SMALL LETTER TROKUTASTI A]
                    return "a";
                case '\uA732': // Ꜳ  [LATIN CAPITAL LETTER AA]
                    return "AA";
                case '\u16F7': // ᛷ  [RUNIC LETTER FRANKS CASKET AC]
                    return "AC";
                case '\u00C6': // Æ  [LATIN CAPITAL LETTER AE]
                case '\u01E2': // Ǣ  [LATIN CAPITAL LETTER AE WITH MACRON]
                case '\u01FC': // Ǽ  [LATIN CAPITAL LETTER AE WITH ACUTE]
                case '\u1D01': // ᴁ  [LATIN LETTER SMALL CAPITAL AE]
                case '\u16C5': // ᛅ  [RUNIC LETTER LONG-BRANCH-AR AE]
                    return "AE";
                case '\uA734': // Ꜵ  [LATIN CAPITAL LETTER AO]
                    return "AO";
                case '\uA736': // Ꜷ  [LATIN CAPITAL LETTER AU]
                    return "AU";
                case '\uA738': // Ꜹ  [LATIN CAPITAL LETTER AV]
                case '\uA73A': // Ꜻ  [LATIN CAPITAL LETTER AV WITH HORIZONTAL BAR]
                    return "AV";
                case '\uA73C': // Ꜽ  [LATIN CAPITAL LETTER AY]
                    return "AY";
                case '\u249C': // ⒜  [PARENTHESIZED LATIN SMALL LETTER A]
                    return "(a)";
                case '\uA733': // ꜳ  [LATIN SMALL LETTER AA]
                    return "aa";
                case '\u00E6': // æ  [LATIN SMALL LETTER AE]
                case '\u01E3': // ǣ  [LATIN SMALL LETTER AE WITH MACRON]
                case '\u01FD': // ǽ  [LATIN SMALL LETTER AE WITH ACUTE]
                case '\u1D02': // ᴂ  [LATIN SMALL LETTER TURNED AE]
                    return "ae";
                case '\uA735': // ꜵ  [LATIN SMALL LETTER AO]
                    return "ao";
                case '\uA737': // ꜷ  [LATIN SMALL LETTER AU]
                    return "au";
                case '\uA739': // ꜹ  [LATIN SMALL LETTER AV]
                case '\uA73B': // ꜻ  [LATIN SMALL LETTER AV WITH HORIZONTAL BAR]
                    return "av";
                case '\uA73D': // ꜽ  [LATIN SMALL LETTER AY]
                    return "ay";
                case '\u0181': // Ɓ  [LATIN CAPITAL LETTER B WITH HOOK]
                case '\u0182': // Ƃ  [LATIN CAPITAL LETTER B WITH TOPBAR]
                case '\u0243': // Ƀ  [LATIN CAPITAL LETTER B WITH STROKE]
                case '\u0299': // ʙ  [LATIN LETTER SMALL CAPITAL B]
                case '\u1D03': // ᴃ  [LATIN LETTER SMALL CAPITAL BARRED B]
                case '\u1E02': // Ḃ  [LATIN CAPITAL LETTER B WITH DOT ABOVE]
                case '\u1E04': // Ḅ  [LATIN CAPITAL LETTER B WITH DOT BELOW]
                case '\u1E06': // Ḇ  [LATIN CAPITAL LETTER B WITH LINE BELOW]
                case '\u24B7': // Ⓑ  [CIRCLED LATIN CAPITAL LETTER B]
                case '\uFF22': // Ｂ  [FULLWIDTH LATIN CAPITAL LETTER B]
                case '\u0392': // Β  [GREEK CAPITAL LETTER BETA]
                case '\uA7B4': // Ꞵ  [LATIN CAPITAL LETTER BETA]
                case '\u0411': // Б  [CYRILLIC CAPITAL LETTER BE]
                case '\u2C01': // Ⰱ  [GLAGOLITIC CAPITAL LETTER BUKY]
                case '\u16D2': // ᛒ  [RUNIC LETTER BERKANAN BEORC BJARKAN B]
                case '\u16D3': // ᛓ  [RUNIC LETTER SHORT-TWIG-BJARKAN B]
                    return "B";
                case '\u0180': // ƀ  [LATIN SMALL LETTER B WITH STROKE]
                case '\u0183': // ƃ  [LATIN SMALL LETTER B WITH TOPBAR]
                case '\u0253': // ɓ  [LATIN SMALL LETTER B WITH HOOK]
                case '\u1D6C': // ᵬ  [LATIN SMALL LETTER B WITH MIDDLE TILDE]
                case '\u1D80': // ᶀ  [LATIN SMALL LETTER B WITH PALATAL HOOK]
                case '\u1E03': // ḃ  [LATIN SMALL LETTER B WITH DOT ABOVE]
                case '\u1E05': // ḅ  [LATIN SMALL LETTER B WITH DOT BELOW]
                case '\u1E07': // ḇ  [LATIN SMALL LETTER B WITH LINE BELOW]
                case '\u24D1': // ⓑ  [CIRCLED LATIN SMALL LETTER B]
                case '\uFF42': // ｂ  [FULLWIDTH LATIN SMALL LETTER B]
                case '\u03B2': // β  [GREEK SMALL LETTER BETA]
                case '\u03D0': // ϐ  [GREEK BETA SYMBOL]
                case '\u1D5D': // ᵝ  [MODIFIER LETTER SMALL BETA]
                case '\u1D66': // ᵦ  [GREEK SUBSCRIPT SMALL LETTER BETA]
                case '\uA7B5': // ꞵ  [LATIN SMALL LETTER BETA]
                case '\u0431': // б  [CYRILLIC SMALL LETTER BE]
                case '\u2C31': // ⰱ  [GLAGOLITIC SMALL LETTER BUKY]
                    return "b";
                case '\u249D': // ⒝  [PARENTHESIZED LATIN SMALL LETTER B]
                    return "(b)";
                case '\u00C7': // Ç  [LATIN CAPITAL LETTER C WITH CEDILLA]
                case '\u0106': // Ć  [LATIN CAPITAL LETTER C WITH ACUTE]
                case '\u0108': // Ĉ  [LATIN CAPITAL LETTER C WITH CIRCUMFLEX]
                case '\u010A': // Ċ  [LATIN CAPITAL LETTER C WITH DOT ABOVE]
                case '\u010C': // Č  [LATIN CAPITAL LETTER C WITH CARON]
                case '\u0187': // Ƈ  [LATIN CAPITAL LETTER C WITH HOOK]
                case '\u023B': // Ȼ  [LATIN CAPITAL LETTER C WITH STROKE]
                case '\u0297': // ʗ  [LATIN LETTER STRETCHED C]
                case '\u1D04': // ᴄ  [LATIN LETTER SMALL CAPITAL C]
                case '\u1E08': // Ḉ  [LATIN CAPITAL LETTER C WITH CEDILLA AND ACUTE]
                case '\u24B8': // Ⓒ  [CIRCLED LATIN CAPITAL LETTER C]
                case '\uFF23': // Ｃ  [FULLWIDTH LATIN CAPITAL LETTER C]
                case '\u16CD': // ᛍ  [RUNIC LETTER C]
                case '\u16B3': // ᚳ  [RUNIC LETTER CEN]
                    return "C";
                case '\u00E7': // ç  [LATIN SMALL LETTER C WITH CEDILLA]
                case '\u0107': // ć  [LATIN SMALL LETTER C WITH ACUTE]
                case '\u0109': // ĉ  [LATIN SMALL LETTER C WITH CIRCUMFLEX]
                case '\u010B': // ċ  [LATIN SMALL LETTER C WITH DOT ABOVE]
                case '\u010D': // č  [LATIN SMALL LETTER C WITH CARON]
                case '\u0188': // ƈ  [LATIN SMALL LETTER C WITH HOOK]
                case '\u023C': // ȼ  [LATIN SMALL LETTER C WITH STROKE]
                case '\u0255': // ɕ  [LATIN SMALL LETTER C WITH CURL]
                case '\u1E09': // ḉ  [LATIN SMALL LETTER C WITH CEDILLA AND ACUTE]
                case '\u2184': // ↄ  [LATIN SMALL LETTER REVERSED C]
                case '\u24D2': // ⓒ  [CIRCLED LATIN SMALL LETTER C]
                case '\uA73E': // Ꜿ  [LATIN CAPITAL LETTER REVERSED C WITH DOT]
                case '\uA73F': // ꜿ  [LATIN SMALL LETTER REVERSED C WITH DOT]
                case '\uFF43': // ｃ  [FULLWIDTH LATIN SMALL LETTER C]
                case '\u00A2': // ¢  [CENT SIGN]
                    return "c";
                case '\u249E': // ⒞  [PARENTHESIZED LATIN SMALL LETTER C]
                    return "(c)";
                case '\u00D0': // Ð  [LATIN CAPITAL LETTER ETH]
                case '\u010E': // Ď  [LATIN CAPITAL LETTER D WITH CARON]
                case '\u0110': // Đ  [LATIN CAPITAL LETTER D WITH STROKE]
                case '\u0189': // Ɖ  [LATIN CAPITAL LETTER AFRICAN D]
                case '\u018A': // Ɗ  [LATIN CAPITAL LETTER D WITH HOOK]
                case '\u018B': // Ƌ  [LATIN CAPITAL LETTER D WITH TOPBAR]
                case '\u1D05': // ᴅ  [LATIN LETTER SMALL CAPITAL D]
                case '\u1D06': // ᴆ  [LATIN LETTER SMALL CAPITAL ETH]
                case '\u1E0A': // Ḋ  [LATIN CAPITAL LETTER D WITH DOT ABOVE]
                case '\u1E0C': // Ḍ  [LATIN CAPITAL LETTER D WITH DOT BELOW]
                case '\u1E0E': // Ḏ  [LATIN CAPITAL LETTER D WITH LINE BELOW]
                case '\u1E10': // Ḑ  [LATIN CAPITAL LETTER D WITH CEDILLA]
                case '\u1E12': // Ḓ  [LATIN CAPITAL LETTER D WITH CIRCUMFLEX BELOW]
                case '\u24B9': // Ⓓ  [CIRCLED LATIN CAPITAL LETTER D]
                case '\uA779': // Ꝺ  [LATIN CAPITAL LETTER INSULAR D]
                case '\uFF24': // Ｄ  [FULLWIDTH LATIN CAPITAL LETTER D]
                case '\u0394': // Δ  [GREEK CAPITAL LETTER DELTA]
                case '\u2C86': // Ⲇ  [COPTIC CAPITAL LETTER DALDA]
                case '\u0414': // Д  [CYRILLIC CAPITAL LETTER DE]
                case '\u2C04': // Ⰴ  [GLAGOLITIC CAPITAL LETTER DOBRO]
                case '\u16DE': // ᛞ  [RUNIC LETTER DAGAZ DAEG D]
                case '\u16D1': // ᛑ  [RUNIC LETTER D]
                    return "D";
                case '\u00F0': // ð  [LATIN SMALL LETTER ETH]
                case '\u010F': // ď  [LATIN SMALL LETTER D WITH CARON]
                case '\u0111': // đ  [LATIN SMALL LETTER D WITH STROKE]
                case '\u018C': // ƌ  [LATIN SMALL LETTER D WITH TOPBAR]
                case '\u0221': // ȡ  [LATIN SMALL LETTER D WITH CURL]
                case '\u0256': // ɖ  [LATIN SMALL LETTER D WITH TAIL]
                case '\u0257': // ɗ  [LATIN SMALL LETTER D WITH HOOK]
                case '\u1D6D': // ᵭ  [LATIN SMALL LETTER D WITH MIDDLE TILDE]
                case '\u1D81': // ᶁ  [LATIN SMALL LETTER D WITH PALATAL HOOK]
                case '\u1D91': // ᶑ  [LATIN SMALL LETTER D WITH HOOK AND TAIL]
                case '\u1E0B': // ḋ  [LATIN SMALL LETTER D WITH DOT ABOVE]
                case '\u1E0D': // ḍ  [LATIN SMALL LETTER D WITH DOT BELOW]
                case '\u1E0F': // ḏ  [LATIN SMALL LETTER D WITH LINE BELOW]
                case '\u1E11': // ḑ  [LATIN SMALL LETTER D WITH CEDILLA]
                case '\u1E13': // ḓ  [LATIN SMALL LETTER D WITH CIRCUMFLEX BELOW]
                case '\u24D3': // ⓓ  [CIRCLED LATIN SMALL LETTER D]
                case '\uA77A': // ꝺ  [LATIN SMALL LETTER INSULAR D]
                case '\uFF44': // ｄ  [FULLWIDTH LATIN SMALL LETTER D]
                case '\u03B4': // δ  [GREEK SMALL LETTER DELTA]
                case '\u1D5F': // ᵟ  [MODIFIER LETTER SMALL DELTA]
                case '\u2C87': // ⲇ  [COPTIC SMALL LETTER DALDA]
                case '\u1E9F': // ẟ  [LATIN SMALL LETTER DELTA]
                case '\u018D': // ƍ  [LATIN SMALL LETTER TURNED DELTA]
                case '\u0434': // д  [CYRILLIC SMALL LETTER DE]
                case '\u1C81': // ᲁ  [CYRILLIC SMALL LETTER LONG-LEGGED DE]
                case '\u2C34': // ⰴ  [GLAGOLITIC SMALL LETTER DOBRO]
                    return "d";
                case '\u01C4': // Ǆ  [LATIN CAPITAL LETTER DZ WITH CARON]
                case '\u01F1': // Ǳ  [LATIN CAPITAL LETTER DZ]
                case '\uA682': // Ꚃ  [CYRILLIC CAPITAL LETTER DZWE]
                case '\u0405': // Ѕ  [CYRILLIC CAPITAL LETTER DZE]
                case '\uA644': // Ꙅ  [CYRILLIC CAPITAL LETTER REVERSED DZE]
                case '\u0402': // Ђ  [CYRILLIC CAPITAL LETTER DJE]
                case '\u0403': // Ѓ  [CYRILLIC CAPITAL LETTER GJE]
                case '\uA648': // Ꙉ  [CYRILLIC CAPITAL LETTER DJERV]
                case '\u040F': // Џ  [CYRILLIC CAPITAL LETTER DZHE]
                case '\uA642': // Ꙃ  [CYRILLIC CAPITAL LETTER DZELO]
                case '\u2C07': // Ⰷ  [GLAGOLITIC CAPITAL LETTER DZELO]
                case '\u2C0C': // Ⰼ  [GLAGOLITIC CAPITAL LETTER DJERVI]
                    return "DZ";
                case '\u01C5': // ǅ  [LATIN CAPITAL LETTER D WITH SMALL LETTER Z WITH CARON]
                case '\u01F2': // ǲ  [LATIN CAPITAL LETTER D WITH SMALL LETTER Z]
                    return "Dz";
                case '\u249F': // ⒟  [PARENTHESIZED LATIN SMALL LETTER D]
                    return "(d)";
                case '\u0238': // ȸ  [LATIN SMALL LETTER DB DIGRAPH]
                    return "db";
                case '\u01C6': // ǆ  [LATIN SMALL LETTER DZ WITH CARON]
                case '\u01F3': // ǳ  [LATIN SMALL LETTER DZ]
                case '\u02A3': // ʣ  [LATIN SMALL LETTER DZ DIGRAPH]
                case '\u02A5': // ʥ  [LATIN SMALL LETTER DZ DIGRAPH WITH CURL]
                case '\uA683': // ꚃ  [CYRILLIC SMALL LETTER DZWE]
                case '\u0455': // ѕ  [CYRILLIC SMALL LETTER DZE]
                case '\uA645': // ꙅ  [CYRILLIC SMALL LETTER REVERSED DZE]
                case '\u0452': // ђ  [CYRILLIC SMALL LETTER DJE]
                case '\u0453': // ѓ  [CYRILLIC SMALL LETTER GJE]
                case '\uA649': // ꙉ  [CYRILLIC SMALL LETTER DJERV]
                case '\u045F': // џ  [CYRILLIC SMALL LETTER DZHE]
                case '\uA643': // ꙃ  [CYRILLIC SMALL LETTER DZELO]
                case '\u2C37': // ⰷ  [GLAGOLITIC SMALL LETTER DZELO]
                case '\u2C3C': // ⰼ  [GLAGOLITIC SMALL LETTER DJERVI]
                    return "dz";
                case '\u00C8': // È  [LATIN CAPITAL LETTER E WITH GRAVE]
                case '\u00C9': // É  [LATIN CAPITAL LETTER E WITH ACUTE]
                case '\u00CA': // Ê  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX]
                case '\u00CB': // Ë  [LATIN CAPITAL LETTER E WITH DIAERESIS]
                case '\u0112': // Ē  [LATIN CAPITAL LETTER E WITH MACRON]
                case '\u0114': // Ĕ  [LATIN CAPITAL LETTER E WITH BREVE]
                case '\u0116': // Ė  [LATIN CAPITAL LETTER E WITH DOT ABOVE]
                case '\u0118': // Ę  [LATIN CAPITAL LETTER E WITH OGONEK]
                case '\u011A': // Ě  [LATIN CAPITAL LETTER E WITH CARON]
                case '\u018E': // Ǝ  [LATIN CAPITAL LETTER REVERSED E]
                case '\u0190': // Ɛ  [LATIN CAPITAL LETTER OPEN E]
                case '\u0204': // Ȅ  [LATIN CAPITAL LETTER E WITH DOUBLE GRAVE]
                case '\u0206': // Ȇ  [LATIN CAPITAL LETTER E WITH INVERTED BREVE]
                case '\u0228': // Ȩ  [LATIN CAPITAL LETTER E WITH CEDILLA]
                case '\u0246': // Ɇ  [LATIN CAPITAL LETTER E WITH STROKE]
                case '\u1D07': // ᴇ  [LATIN LETTER SMALL CAPITAL E]
                case '\u1E14': // Ḕ  [LATIN CAPITAL LETTER E WITH MACRON AND GRAVE]
                case '\u1E16': // Ḗ  [LATIN CAPITAL LETTER E WITH MACRON AND ACUTE]
                case '\u1E18': // Ḙ  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX BELOW]
                case '\u1E1A': // Ḛ  [LATIN CAPITAL LETTER E WITH TILDE BELOW]
                case '\u1E1C': // Ḝ  [LATIN CAPITAL LETTER E WITH CEDILLA AND BREVE]
                case '\u1EB8': // Ẹ  [LATIN CAPITAL LETTER E WITH DOT BELOW]
                case '\u1EBA': // Ẻ  [LATIN CAPITAL LETTER E WITH HOOK ABOVE]
                case '\u1EBC': // Ẽ  [LATIN CAPITAL LETTER E WITH TILDE]
                case '\u1EBE': // Ế  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND ACUTE]
                case '\u1EC0': // Ề  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND GRAVE]
                case '\u1EC2': // Ể  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE]
                case '\u1EC4': // Ễ  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND TILDE]
                case '\u1EC6': // Ệ  [LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND DOT BELOW]
                case '\u24BA': // Ⓔ  [CIRCLED LATIN CAPITAL LETTER E]
                case '\u2C7B': // ⱻ  [LATIN LETTER SMALL CAPITAL TURNED E]
                case '\uFF25': // Ｅ  [FULLWIDTH LATIN CAPITAL LETTER E]
                case '\u018F': // Ə  [LATIN CAPITAL LETTER SCHWA]
                case '\u0259': // ə  [LATIN SMALL LETTER SCHWA]
                case '\u025A': // ɚ  [LATIN SMALL LETTER SCHWA WITH HOOK]
                case '\u1D95': // ᶕ  [LATIN SMALL LETTER SCHWA WITH RETROFLEX HOOK]
                case '\u2094': // ₔ  [LATIN SUBSCRIPT SMALL LETTER SCHWA]
                case '\u0395': // Ε  [GREEK CAPITAL LETTER EPSILON]
                case '\u0388': // Έ  [GREEK CAPITAL LETTER EPSILON WITH TONOS]
                case '\u2C88': // Ⲉ  [COPTIC CAPITAL LETTER EIE]
                case '\u0415': // Е  [CYRILLIC CAPITAL LETTER IE]
                case '\u0400': // Ѐ  [CYRILLIC CAPITAL LETTER IE WITH GRAVE]
                case '\u042D': // Э  [CYRILLIC CAPITAL LETTER E]
                case '\u0466': // Ѧ  [CYRILLIC CAPITAL LETTER LITTLE YUS]
                case '\uA658': // Ꙙ  [CYRILLIC CAPITAL LETTER CLOSED LITTLE YUS]
                case '\u2C05': // Ⰵ  [GLAGOLITIC CAPITAL LETTER YESTU]
                case '\u2C24': // Ⱔ  [GLAGOLITIC CAPITAL LETTER SMALL YUS]
                case '\u2C25': // Ⱕ  [GLAGOLITIC CAPITAL LETTER SMALL YUS WITH TAIL]
                case '\u16D6': // ᛖ  [RUNIC LETTER EHWAZ EH E]
                case '\u16C2': // ᛂ  [RUNIC LETTER E]
                case '\u16B6': // ᚶ  [RUNIC LETTER ENG]
                case '\u16E0': // ᛠ  [RUNIC LETTER EAR]
                    return "E";
                case '\u16F6': // ᛶ  [RUNIC LETTER FRANKS CASKET EH]
                    return "EH";
                case '\u00E8': // è  [LATIN SMALL LETTER E WITH GRAVE]
                case '\u00E9': // é  [LATIN SMALL LETTER E WITH ACUTE]
                case '\u00EA': // ê  [LATIN SMALL LETTER E WITH CIRCUMFLEX]
                case '\u00EB': // ë  [LATIN SMALL LETTER E WITH DIAERESIS]
                case '\u0113': // ē  [LATIN SMALL LETTER E WITH MACRON]
                case '\u0115': // ĕ  [LATIN SMALL LETTER E WITH BREVE]
                case '\u0117': // ė  [LATIN SMALL LETTER E WITH DOT ABOVE]
                case '\u0119': // ę  [LATIN SMALL LETTER E WITH OGONEK]
                case '\u011B': // ě  [LATIN SMALL LETTER E WITH CARON]
                case '\u01DD': // ǝ  [LATIN SMALL LETTER TURNED E]
                case '\u0205': // ȅ  [LATIN SMALL LETTER E WITH DOUBLE GRAVE]
                case '\u0207': // ȇ  [LATIN SMALL LETTER E WITH INVERTED BREVE]
                case '\u0229': // ȩ  [LATIN SMALL LETTER E WITH CEDILLA]
                case '\u0247': // ɇ  [LATIN SMALL LETTER E WITH STROKE]
                case '\u0258': // ɘ  [LATIN SMALL LETTER REVERSED E]
                case '\u025B': // ɛ  [LATIN SMALL LETTER OPEN E]
                case '\u025C': // ɜ  [LATIN SMALL LETTER REVERSED OPEN E]
                case '\u025D': // ɝ  [LATIN SMALL LETTER REVERSED OPEN E WITH HOOK]
                case '\u025E': // ɞ  [LATIN SMALL LETTER CLOSED REVERSED OPEN E]
                case '\u029A': // ʚ  [LATIN SMALL LETTER CLOSED OPEN E]
                case '\u1D08': // ᴈ  [LATIN SMALL LETTER TURNED OPEN E]
                case '\u1D92': // ᶒ  [LATIN SMALL LETTER E WITH RETROFLEX HOOK]
                case '\u1D93': // ᶓ  [LATIN SMALL LETTER OPEN E WITH RETROFLEX HOOK]
                case '\u1D94': // ᶔ  [LATIN SMALL LETTER REVERSED OPEN E WITH RETROFLEX HOOK]
                case '\u1E15': // ḕ  [LATIN SMALL LETTER E WITH MACRON AND GRAVE]
                case '\u1E17': // ḗ  [LATIN SMALL LETTER E WITH MACRON AND ACUTE]
                case '\u1E19': // ḙ  [LATIN SMALL LETTER E WITH CIRCUMFLEX BELOW]
                case '\u1E1B': // ḛ  [LATIN SMALL LETTER E WITH TILDE BELOW]
                case '\u1E1D': // ḝ  [LATIN SMALL LETTER E WITH CEDILLA AND BREVE]
                case '\u1EB9': // ẹ  [LATIN SMALL LETTER E WITH DOT BELOW]
                case '\u1EBB': // ẻ  [LATIN SMALL LETTER E WITH HOOK ABOVE]
                case '\u1EBD': // ẽ  [LATIN SMALL LETTER E WITH TILDE]
                case '\u1EBF': // ế  [LATIN SMALL LETTER E WITH CIRCUMFLEX AND ACUTE]
                case '\u1EC1': // ề  [LATIN SMALL LETTER E WITH CIRCUMFLEX AND GRAVE]
                case '\u1EC3': // ể  [LATIN SMALL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE]
                case '\u1EC5': // ễ  [LATIN SMALL LETTER E WITH CIRCUMFLEX AND TILDE]
                case '\u1EC7': // ệ  [LATIN SMALL LETTER E WITH CIRCUMFLEX AND DOT BELOW]
                case '\u2091': // ₑ  [LATIN SUBSCRIPT SMALL LETTER E]
                case '\u24D4': // ⓔ  [CIRCLED LATIN SMALL LETTER E]
                case '\u2C78': // ⱸ  [LATIN SMALL LETTER E WITH NOTCH]
                case '\uFF45': // ｅ  [FULLWIDTH LATIN SMALL LETTER E]
                case '\u03B5': // ε  [GREEK SMALL LETTER EPSILON]
                case '\u03AD': // έ  [GREEK SMALL LETTER EPSILON WITH TONOS]
                case '\u03F5': // ϵ  [GREEK LUNATE EPSILON SYMBOL]
                case '\u03F6': // ϶  [GREEK REVERSED LUNATE EPSILON SYMBOL]
                case '\u2C89': // ⲉ  [COPTIC SMALL LETTER EIE]
                case '\u0435': // е  [CYRILLIC SMALL LETTER IE]
                case '\u0450': // ѐ  [CYRILLIC SMALL LETTER IE WITH GRAVE]
                case '\u044D': // э  [CYRILLIC SMALL LETTER E]
                case '\u0467': // ѧ  [CYRILLIC SMALL LETTER LITTLE YUS]
                case '\uA659': // ꙙ  [CYRILLIC SMALL LETTER CLOSED LITTLE YUS]
                case '\u2C35': // ⰵ  [GLAGOLITIC SMALL LETTER YESTU]
                case '\u2C54': // ⱔ  [GLAGOLITIC SMALL LETTER SMALL YUS]
                case '\u2C55': // ⱕ  [GLAGOLITIC SMALL LETTER SMALL YUS WITH TAIL]
                    return "e";
                case '\u24A0': // ⒠  [PARENTHESIZED LATIN SMALL LETTER E]
                    return "(e)";
                case '\u0191': // Ƒ  [LATIN CAPITAL LETTER F WITH HOOK]
                case '\u1E1E': // Ḟ  [LATIN CAPITAL LETTER F WITH DOT ABOVE]
                case '\u24BB': // Ⓕ  [CIRCLED LATIN CAPITAL LETTER F]
                case '\uA730': // ꜰ  [LATIN LETTER SMALL CAPITAL F]
                case '\uA77B': // Ꝼ  [LATIN CAPITAL LETTER INSULAR F]
                case '\uA7FB': // ꟻ  [LATIN EPIGRAPHIC LETTER REVERSED F]
                case '\uFF26': // Ｆ  [FULLWIDTH LATIN CAPITAL LETTER F]
                case '\u03A6': // Φ  [GREEK CAPITAL LETTER PHI]
                case '\u2CAA': // Ⲫ  [COPTIC CAPITAL LETTER FI]
                case '\u0424': // Ф  [CYRILLIC CAPITAL LETTER EF]
                case '\u2C17': // Ⱇ  [GLAGOLITIC CAPITAL LETTER FRITU]
                case '\u16A0': // ᚠ  [RUNIC LETTER FEHU FEOH FE F]
                    return "F";
                case '\u0192': // ƒ  [LATIN SMALL LETTER F WITH HOOK]
                case '\u1D6E': // ᵮ  [LATIN SMALL LETTER F WITH MIDDLE TILDE]
                case '\u1D82': // ᶂ  [LATIN SMALL LETTER F WITH PALATAL HOOK]
                case '\u1E1F': // ḟ  [LATIN SMALL LETTER F WITH DOT ABOVE]
                case '\u1E9B': // ẛ  [LATIN SMALL LETTER LONG S WITH DOT ABOVE]
                case '\u24D5': // ⓕ  [CIRCLED LATIN SMALL LETTER F]
                case '\uA77C': // ꝼ  [LATIN SMALL LETTER INSULAR F]
                case '\uFF46': // ｆ  [FULLWIDTH LATIN SMALL LETTER F]
                case '\u03C6': // φ  [GREEK SMALL LETTER PHI]
                case '\u2CAB': // ⲫ  [COPTIC SMALL LETTER FI]
                case '\u2C77': // ⱷ  [LATIN SMALL LETTER TAILLESS PHI]
                case '\u0278': // ɸ  [LATIN SMALL LETTER PHI]
                case '\u1D60': // ᵠ  [MODIFIER LETTER SMALL GREEK PHI]
                case '\u1D69': // ᵩ  [GREEK SUBSCRIPT SMALL LETTER PHI]
                case '\u1DB2': // ᶲ  [MODIFIER LETTER SMALL PHI]
                case '\u0444': // ф  [CYRILLIC SMALL LETTER EF]
                case '\u2C47': // ⱇ  [GLAGOLITIC SMALL LETTER FRITU]
                    return "f";
                case '\u24A1': // ⒡  [PARENTHESIZED LATIN SMALL LETTER F]
                    return "(f)";
                case '\uFB00': // ﬀ  [LATIN SMALL LIGATURE FF]
                    return "ff";
                case '\uFB03': // ﬃ  [LATIN SMALL LIGATURE FFI]
                    return "ffi";
                case '\uFB04': // ﬄ  [LATIN SMALL LIGATURE FFL]
                    return "ffl";
                case '\uFB01': // ﬁ  [LATIN SMALL LIGATURE FI]
                    return "fi";
                case '\uFB02': // ﬂ  [LATIN SMALL LIGATURE FL]
                    return "fl";
                case '\u011C': // Ĝ  [LATIN CAPITAL LETTER G WITH CIRCUMFLEX]
                case '\u011E': // Ğ  [LATIN CAPITAL LETTER G WITH BREVE]
                case '\u0120': // Ġ  [LATIN CAPITAL LETTER G WITH DOT ABOVE]
                case '\u0122': // Ģ  [LATIN CAPITAL LETTER G WITH CEDILLA]
                case '\u0193': // Ɠ  [LATIN CAPITAL LETTER G WITH HOOK]
                case '\u01E4': // Ǥ  [LATIN CAPITAL LETTER G WITH STROKE]
                case '\u01E5': // ǥ  [LATIN SMALL LETTER G WITH STROKE]
                case '\u01E6': // Ǧ  [LATIN CAPITAL LETTER G WITH CARON]
                case '\u01E7': // ǧ  [LATIN SMALL LETTER G WITH CARON]
                case '\u01F4': // Ǵ  [LATIN CAPITAL LETTER G WITH ACUTE]
                case '\u0262': // ɢ  [LATIN LETTER SMALL CAPITAL G]
                case '\u029B': // ʛ  [LATIN LETTER SMALL CAPITAL G WITH HOOK]
                case '\u1E20': // Ḡ  [LATIN CAPITAL LETTER G WITH MACRON]
                case '\u24BC': // Ⓖ  [CIRCLED LATIN CAPITAL LETTER G]
                case '\uA77D': // Ᵹ  [LATIN CAPITAL LETTER INSULAR G]
                case '\uA77E': // Ꝿ  [LATIN CAPITAL LETTER TURNED INSULAR G]
                case '\uFF27': // Ｇ  [FULLWIDTH LATIN CAPITAL LETTER G]
                case '\u0393': // Γ  [GREEK CAPITAL LETTER GAMMA]
                case '\u1D26': // ᴦ  [GREEK LETTER SMALL CAPITAL GAMMA]
                case '\u2C84': // Ⲅ  [COPTIC CAPITAL LETTER GAMMA]
                case '\u0413': // Г  [CYRILLIC CAPITAL LETTER GHE]
                case '\u0490': // Ґ  [CYRILLIC CAPITAL LETTER GHE WITH UPTURN]
                case '\u2C03': // Ⰳ  [GLAGOLITIC CAPITAL LETTER GLAGOLI]
                case '\u16B7': // ᚷ  [RUNIC LETTER GEBO GYFU G]
                case '\u16B5': // ᚵ  [RUNIC LETTER G]
                case '\u16B8': // ᚸ  [RUNIC LETTER GAR]
                    return "G";
                case '\u011D': // ĝ  [LATIN SMALL LETTER G WITH CIRCUMFLEX]
                case '\u011F': // ğ  [LATIN SMALL LETTER G WITH BREVE]
                case '\u0121': // ġ  [LATIN SMALL LETTER G WITH DOT ABOVE]
                case '\u0123': // ģ  [LATIN SMALL LETTER G WITH CEDILLA]
                case '\u01F5': // ǵ  [LATIN SMALL LETTER G WITH ACUTE]
                case '\u0260': // ɠ  [LATIN SMALL LETTER G WITH HOOK]
                case '\u0261': // ɡ  [LATIN SMALL LETTER SCRIPT G]
                case '\u1D77': // ᵷ  [LATIN SMALL LETTER TURNED G]
                case '\u1D79': // ᵹ  [LATIN SMALL LETTER INSULAR G]
                case '\u1D83': // ᶃ  [LATIN SMALL LETTER G WITH PALATAL HOOK]
                case '\u1E21': // ḡ  [LATIN SMALL LETTER G WITH MACRON]
                case '\u24D6': // ⓖ  [CIRCLED LATIN SMALL LETTER G]
                case '\uA77F': // ꝿ  [LATIN SMALL LETTER TURNED INSULAR G]
                case '\uFF47': // ｇ  [FULLWIDTH LATIN SMALL LETTER G]
                case '\u03B3': // γ  [GREEK SMALL LETTER GAMMA]
                case '\u1D5E': // ᵞ  [MODIFIER LETTER SMALL GREEK GAMMA]
                case '\u1D67': // ᵧ  [GREEK SUBSCRIPT SMALL LETTER GAMMA]
                case '\u2C85': // ⲅ  [COPTIC SMALL LETTER GAMMA]
                case '\u0433': // г  [CYRILLIC SMALL LETTER GHE]
                case '\u0491': // ґ  [CYRILLIC SMALL LETTER GHE WITH UPTURN]
                case '\u2C33': // ⰳ  [GLAGOLITIC SMALL LETTER GLAGOLI]
                    return "g";
                case '\u24A2': // ⒢  [PARENTHESIZED LATIN SMALL LETTER G]
                    return "(g)";
                case '\u0124': // Ĥ  [LATIN CAPITAL LETTER H WITH CIRCUMFLEX]
                case '\u0126': // Ħ  [LATIN CAPITAL LETTER H WITH STROKE]
                case '\u021E': // Ȟ  [LATIN CAPITAL LETTER H WITH CARON]
                case '\u029C': // ʜ  [LATIN LETTER SMALL CAPITAL H]
                case '\u1E22': // Ḣ  [LATIN CAPITAL LETTER H WITH DOT ABOVE]
                case '\u1E24': // Ḥ  [LATIN CAPITAL LETTER H WITH DOT BELOW]
                case '\u1E26': // Ḧ  [LATIN CAPITAL LETTER H WITH DIAERESIS]
                case '\u1E28': // Ḩ  [LATIN CAPITAL LETTER H WITH CEDILLA]
                case '\u1E2A': // Ḫ  [LATIN CAPITAL LETTER H WITH BREVE BELOW]
                case '\u24BD': // Ⓗ  [CIRCLED LATIN CAPITAL LETTER H]
                case '\u2C67': // Ⱨ  [LATIN CAPITAL LETTER H WITH DESCENDER]
                case '\u2C75': // Ⱶ  [LATIN CAPITAL LETTER HALF H]
                case '\uFF28': // Ｈ  [FULLWIDTH LATIN CAPITAL LETTER H]
                case '\u0370': // Ͱ  [GREEK CAPITAL LETTER HETA]
                case '\u16BA': // ᚺ  [RUNIC LETTER HAGLAZ H]
                case '\u16BB': // ᚻ  [RUNIC LETTER HAEGL H]
                case '\u16BC': // ᚼ  [RUNIC LETTER LONG-BRANCH-HAGALL H]
                case '\u16BD': // ᚽ  [RUNIC LETTER SHORT-TWIG-HAGALL H]
                    return "H";
                case '\u0125': // ĥ  [LATIN SMALL LETTER H WITH CIRCUMFLEX]
                case '\u0127': // ħ  [LATIN SMALL LETTER H WITH STROKE]
                case '\u021F': // ȟ  [LATIN SMALL LETTER H WITH CARON]
                case '\u0265': // ɥ  [LATIN SMALL LETTER TURNED H]
                case '\u0266': // ɦ  [LATIN SMALL LETTER H WITH HOOK]
                case '\u02AE': // ʮ  [LATIN SMALL LETTER TURNED H WITH FISHHOOK]
                case '\u02AF': // ʯ  [LATIN SMALL LETTER TURNED H WITH FISHHOOK AND TAIL]
                case '\u1E23': // ḣ  [LATIN SMALL LETTER H WITH DOT ABOVE]
                case '\u1E25': // ḥ  [LATIN SMALL LETTER H WITH DOT BELOW]
                case '\u1E27': // ḧ  [LATIN SMALL LETTER H WITH DIAERESIS]
                case '\u1E29': // ḩ  [LATIN SMALL LETTER H WITH CEDILLA]
                case '\u1E2B': // ḫ  [LATIN SMALL LETTER H WITH BREVE BELOW]
                case '\u1E96': // ẖ  [LATIN SMALL LETTER H WITH LINE BELOW]
                case '\u24D7': // ⓗ  [CIRCLED LATIN SMALL LETTER H]
                case '\u2C68': // ⱨ  [LATIN SMALL LETTER H WITH DESCENDER]
                case '\u2C76': // ⱶ  [LATIN SMALL LETTER HALF H]
                case '\uFF48': // ｈ  [FULLWIDTH LATIN SMALL LETTER H]
                case '\u0371': // ͱ  [GREEK SMALL LETTER HETA]
                    return "h";
                case '\u01F6': // Ƕ  [LATIN CAPITAL LETTER HWAIR]
                    return "HV";
                case '\u24A3': // ⒣  [PARENTHESIZED LATIN SMALL LETTER H]
                    return "(h)";
                case '\u0195': // ƕ  [LATIN SMALL LETTER HV]
                    return "hv";
                case '\u00CC': // Ì  [LATIN CAPITAL LETTER I WITH GRAVE]
                case '\u00CD': // Í  [LATIN CAPITAL LETTER I WITH ACUTE]
                case '\u00CE': // Î  [LATIN CAPITAL LETTER I WITH CIRCUMFLEX]
                case '\u00CF': // Ï  [LATIN CAPITAL LETTER I WITH DIAERESIS]
                case '\u0128': // Ĩ  [LATIN CAPITAL LETTER I WITH TILDE]
                case '\u012A': // Ī  [LATIN CAPITAL LETTER I WITH MACRON]
                case '\u012C': // Ĭ  [LATIN CAPITAL LETTER I WITH BREVE]
                case '\u012E': // Į  [LATIN CAPITAL LETTER I WITH OGONEK]
                case '\u0130': // İ  [LATIN CAPITAL LETTER I WITH DOT ABOVE]
                case '\u0196': // Ɩ  [LATIN CAPITAL LETTER IOTA]
                case '\u0197': // Ɨ  [LATIN CAPITAL LETTER I WITH STROKE]
                case '\u01CF': // Ǐ  [LATIN CAPITAL LETTER I WITH CARON]
                case '\u0208': // Ȉ  [LATIN CAPITAL LETTER I WITH DOUBLE GRAVE]
                case '\u020A': // Ȋ  [LATIN CAPITAL LETTER I WITH INVERTED BREVE]
                case '\u026A': // ɪ  [LATIN LETTER SMALL CAPITAL I]
                case '\u1D7B': // ᵻ  [LATIN SMALL CAPITAL LETTER I WITH STROKE]
                case '\u1E2C': // Ḭ  [LATIN CAPITAL LETTER I WITH TILDE BELOW]
                case '\u1E2E': // Ḯ  [LATIN CAPITAL LETTER I WITH DIAERESIS AND ACUTE]
                case '\u1EC8': // Ỉ  [LATIN CAPITAL LETTER I WITH HOOK ABOVE]
                case '\u1ECA': // Ị  [LATIN CAPITAL LETTER I WITH DOT BELOW]
                case '\u24BE': // Ⓘ  [CIRCLED LATIN CAPITAL LETTER I]
                case '\uA7FE': // ꟾ  [LATIN EPIGRAPHIC LETTER I LONGA]
                case '\uFF29': // Ｉ  [FULLWIDTH LATIN CAPITAL LETTER I]
                case '\u0406': // І  [CYRILLIC CAPITAL LETTER BYELORUSSIAN-UKRAINIAN I]
                case '\u0397': // Η  [GREEK CAPITAL LETTER ETA]
                case '\u0389': // Ή  [GREEK CAPITAL LETTER ETA WITH TONOS]
                case '\u0399': // Ι  [GREEK CAPITAL LETTER IOTA]
                case '\u038A': // Ί  [GREEK CAPITAL LETTER IOTA WITH TONOS]
                case '\u03AA': // Ϊ  [GREEK CAPITAL LETTER IOTA WITH DIALYTIKA]
                case '\u0418': // И  [CYRILLIC CAPITAL LETTER I]
                case '\u040D': // Ѝ  [CYRILLIC CAPITAL LETTER I WITH GRAVE]
                case '\u042B': // Ы  [CYRILLIC CAPITAL LETTER YERU]
                case '\uA650': // Ꙑ  [CYRILLIC CAPITAL LETTER YERU WITH BACK YER]
                case '\uA65E': // Ꙟ  [CYRILLIC CAPITAL LETTER YN]
                case '\u0474': // Ѵ  [CYRILLIC CAPITAL LETTER IZHITSA]
                case '\u0476': // Ѷ  [CYRILLIC CAPITAL LETTER IZHITSA WITH DOUBLE GRAVE ACCENT]
                case '\uA646': // Ꙇ  [CYRILLIC CAPITAL LETTER IOTA]
                case '\u2C09': // Ⰹ  [GLAGOLITIC CAPITAL LETTER IZHE]
                case '\u2C0A': // Ⰺ  [GLAGOLITIC CAPITAL LETTER INITIAL IZHE]
                case '\u2C0B': // Ⰻ  [GLAGOLITIC CAPITAL LETTER I]
                case '\u2C1F': // Ⱏ  [GLAGOLITIC CAPITAL LETTER YERU]
                case '\u2C2B': // Ⱛ  [GLAGOLITIC CAPITAL LETTER IZHITSA]
                case '\u16C1': // ᛁ  [RUNIC LETTER ISAZ IS ISS I]
                case '\u16C7': // ᛇ  [RUNIC LETTER IWAZ EOH]
                    return "I";
                case '\u00EC': // ì  [LATIN SMALL LETTER I WITH GRAVE]
                case '\u00ED': // í  [LATIN SMALL LETTER I WITH ACUTE]
                case '\u00EE': // î  [LATIN SMALL LETTER I WITH CIRCUMFLEX]
                case '\u00EF': // ï  [LATIN SMALL LETTER I WITH DIAERESIS]
                case '\u0129': // ĩ  [LATIN SMALL LETTER I WITH TILDE]
                case '\u012B': // ī  [LATIN SMALL LETTER I WITH MACRON]
                case '\u012D': // ĭ  [LATIN SMALL LETTER I WITH BREVE]
                case '\u012F': // į  [LATIN SMALL LETTER I WITH OGONEK]
                case '\u0131': // ı  [LATIN SMALL LETTER DOTLESS I]
                case '\u01D0': // ǐ  [LATIN SMALL LETTER I WITH CARON]
                case '\u0209': // ȉ  [LATIN SMALL LETTER I WITH DOUBLE GRAVE]
                case '\u020B': // ȋ  [LATIN SMALL LETTER I WITH INVERTED BREVE]
                case '\u0268': // ɨ  [LATIN SMALL LETTER I WITH STROKE]
                case '\u1D09': // ᴉ  [LATIN SMALL LETTER TURNED I]
                case '\u1D62': // ᵢ  [LATIN SUBSCRIPT SMALL LETTER I]
                case '\u1D7C': // ᵼ  [LATIN SMALL LETTER IOTA WITH STROKE]
                case '\u1D96': // ᶖ  [LATIN SMALL LETTER I WITH RETROFLEX HOOK]
                case '\u1E2D': // ḭ  [LATIN SMALL LETTER I WITH TILDE BELOW]
                case '\u1E2F': // ḯ  [LATIN SMALL LETTER I WITH DIAERESIS AND ACUTE]
                case '\u1EC9': // ỉ  [LATIN SMALL LETTER I WITH HOOK ABOVE]
                case '\u1ECB': // ị  [LATIN SMALL LETTER I WITH DOT BELOW]
                case '\u2071': // ⁱ  [SUPERSCRIPT LATIN SMALL LETTER I]
                case '\u24D8': // ⓘ  [CIRCLED LATIN SMALL LETTER I]
                case '\uFF49': // ｉ  [FULLWIDTH LATIN SMALL LETTER I]
                case '\u0456': // і  [CYRILLIC SMALL LETTER BYELORUSSIAN-UKRAINIAN I]
                case '\u03B7': // η  [GREEK SMALL LETTER ETA]
                case '\u03AE': // ή  [GREEK SMALL LETTER ETA WITH TONOS]
                case '\u03B9': // ι  [GREEK SMALL LETTER IOTA]
                case '\u03AF': // ί  [GREEK SMALL LETTER IOTA WITH TONOS]
                case '\u03CA': // ϊ  [GREEK SMALL LETTER IOTA WITH DIALYTIKA]
                case '\u0438': // и  [CYRILLIC SMALL LETTER I]
                case '\u045D': // ѝ  [CYRILLIC SMALL LETTER I WITH GRAVE]
                case '\u044B': // ы  [CYRILLIC SMALL LETTER YERU]
                case '\uA651': // ꙑ  [CYRILLIC SMALL LETTER YERU WITH BACK YER]
                case '\uA65F': // ꙟ  [CYRILLIC SMALL LETTER YN]
                case '\u0475': // ѵ  [CYRILLIC SMALL LETTER IZHITSA]
                case '\u0477': // ѷ  [CYRILLIC SMALL LETTER IZHITSA WITH DOUBLE GRAVE ACCENT]
                case '\uA647': // ꙇ  [CYRILLIC SMALL LETTER IOTA]
                case '\u2C39': // ⰹ  [GLAGOLITIC SMALL LETTER IZHE]
                case '\u2C3A': // ⰺ  [GLAGOLITIC SMALL LETTER INITIAL IZHE]
                case '\u2C3B': // ⰻ  [GLAGOLITIC SMALL LETTER I]
                case '\u2C4F': // ⱏ  [GLAGOLITIC SMALL LETTER YERU]
                case '\u2C5B': // ⱛ  [GLAGOLITIC SMALL LETTER IZHITSA]
                    return "i";
                case '\u0132': // Ĳ  [LATIN CAPITAL LIGATURE IJ]
                    return "IJ";
                case '\u24A4': // ⒤  [PARENTHESIZED LATIN SMALL LETTER I]
                    return "(i)";
                case '\u0133': // ĳ  [LATIN SMALL LIGATURE IJ]
                    return "ij";
                case '\u16F5': // ᛵ  [RUNIC LETTER FRANKS CASKET IS]
                    return "IS";
                case '\u0134': // Ĵ  [LATIN CAPITAL LETTER J WITH CIRCUMFLEX]
                case '\u0248': // Ɉ  [LATIN CAPITAL LETTER J WITH STROKE]
                case '\u1D0A': // ᴊ  [LATIN LETTER SMALL CAPITAL J]
                case '\u24BF': // Ⓙ  [CIRCLED LATIN CAPITAL LETTER J]
                case '\uFF2A': // Ｊ  [FULLWIDTH LATIN CAPITAL LETTER J]
                case '\u16C3': // ᛃ  [RUNIC LETTER JERAN J]
                case '\u16C4': // ᛄ  [RUNIC LETTER GER]
                case '\u16E1': // ᛡ  [RUNIC LETTER IOR]
                    return "J";
                case '\u0135': // ĵ  [LATIN SMALL LETTER J WITH CIRCUMFLEX]
                case '\u01F0': // ǰ  [LATIN SMALL LETTER J WITH CARON]
                case '\u0237': // ȷ  [LATIN SMALL LETTER DOTLESS J]
                case '\u0249': // ɉ  [LATIN SMALL LETTER J WITH STROKE]
                case '\u025F': // ɟ  [LATIN SMALL LETTER DOTLESS J WITH STROKE]
                case '\u0284': // ʄ  [LATIN SMALL LETTER DOTLESS J WITH STROKE AND HOOK]
                case '\u029D': // ʝ  [LATIN SMALL LETTER J WITH CROSSED-TAIL]
                case '\u24D9': // ⓙ  [CIRCLED LATIN SMALL LETTER J]
                case '\u2C7C': // ⱼ  [LATIN SUBSCRIPT SMALL LETTER J]
                case '\uFF4A': // ｊ  [FULLWIDTH LATIN SMALL LETTER J]
                    return "j";
                case '\u24A5': // ⒥  [PARENTHESIZED LATIN SMALL LETTER J]
                    return "(j)";
                case '\u0136': // Ķ  [LATIN CAPITAL LETTER K WITH CEDILLA]
                case '\u0198': // Ƙ  [LATIN CAPITAL LETTER K WITH HOOK]
                case '\u01E8': // Ǩ  [LATIN CAPITAL LETTER K WITH CARON]
                case '\u1D0B': // ᴋ  [LATIN LETTER SMALL CAPITAL K]
                case '\u1E30': // Ḱ  [LATIN CAPITAL LETTER K WITH ACUTE]
                case '\u1E32': // Ḳ  [LATIN CAPITAL LETTER K WITH DOT BELOW]
                case '\u1E34': // Ḵ  [LATIN CAPITAL LETTER K WITH LINE BELOW]
                case '\u24C0': // Ⓚ  [CIRCLED LATIN CAPITAL LETTER K]
                case '\u2C69': // Ⱪ  [LATIN CAPITAL LETTER K WITH DESCENDER]
                case '\uA740': // Ꝁ  [LATIN CAPITAL LETTER K WITH STROKE]
                case '\uA742': // Ꝃ  [LATIN CAPITAL LETTER K WITH DIAGONAL STROKE]
                case '\uA744': // Ꝅ  [LATIN CAPITAL LETTER K WITH STROKE AND DIAGONAL STROKE]
                case '\uFF2B': // Ｋ  [FULLWIDTH LATIN CAPITAL LETTER K]
                case '\u039A': // Κ  [GREEK CAPITAL LETTER KAPPA]
                case '\u2C94': // Ⲕ  [COPTIC CAPITAL LETTER KAPA]
                case '\u041A': // К  [CYRILLIC CAPITAL LETTER KA]
                case '\u040C': // Ќ  [CYRILLIC CAPITAL LETTER KJE]
                case '\u2C0D': // Ⰽ  [GLAGOLITIC CAPITAL LETTER KAKO]
                case '\u16B2': // ᚲ  [RUNIC LETTER KAUNA]
                case '\u16B4': // ᚴ  [RUNIC LETTER KAUN K]
                case '\u16E3': // ᛣ  [RUNIC LETTER CALC]
                case '\u16F1': // ᛱ  [RUNIC LETTER K]
                    return "K";
                case '\u16E4': // ᛤ  [RUNIC LETTER CEALC]
                    return "KK";
                case '\u16E2': // ᛢ  [RUNIC LETTER CWEORTH]
                    return "KW";
                case '\u0137': // ķ  [LATIN SMALL LETTER K WITH CEDILLA]
                case '\u0199': // ƙ  [LATIN SMALL LETTER K WITH HOOK]
                case '\u01E9': // ǩ  [LATIN SMALL LETTER K WITH CARON]
                case '\u029E': // ʞ  [LATIN SMALL LETTER TURNED K]
                case '\u1D84': // ᶄ  [LATIN SMALL LETTER K WITH PALATAL HOOK]
                case '\u1E31': // ḱ  [LATIN SMALL LETTER K WITH ACUTE]
                case '\u1E33': // ḳ  [LATIN SMALL LETTER K WITH DOT BELOW]
                case '\u1E35': // ḵ  [LATIN SMALL LETTER K WITH LINE BELOW]
                case '\u24DA': // ⓚ  [CIRCLED LATIN SMALL LETTER K]
                case '\u2C6A': // ⱪ  [LATIN SMALL LETTER K WITH DESCENDER]
                case '\uA741': // ꝁ  [LATIN SMALL LETTER K WITH STROKE]
                case '\uA743': // ꝃ  [LATIN SMALL LETTER K WITH DIAGONAL STROKE]
                case '\uA745': // ꝅ  [LATIN SMALL LETTER K WITH STROKE AND DIAGONAL STROKE]
                case '\uFF4B': // ｋ  [FULLWIDTH LATIN SMALL LETTER K]
                case '\u03BA': // κ  [GREEK SMALL LETTER KAPPA]
                case '\u03F0': // ϰ  [GREEK KAPPA SYMBOL]
                case '\u2C95': // ⲕ  [COPTIC SMALL LETTER KAPA]
                case '\u043A': // к  [CYRILLIC SMALL LETTER KA]
                case '\u045C': // ќ  [CYRILLIC SMALL LETTER KJE]
                case '\u2C3D': // ⰽ  [GLAGOLITIC SMALL LETTER KAKO]
                    return "k";
                case '\u24A6': // ⒦  [PARENTHESIZED LATIN SMALL LETTER K]
                    return "(k)";
                case '\u0139': // Ĺ  [LATIN CAPITAL LETTER L WITH ACUTE]
                case '\u013B': // Ļ  [LATIN CAPITAL LETTER L WITH CEDILLA]
                case '\u013D': // Ľ  [LATIN CAPITAL LETTER L WITH CARON]
                case '\u013F': // Ŀ  [LATIN CAPITAL LETTER L WITH MIDDLE DOT]
                case '\u0141': // Ł  [LATIN CAPITAL LETTER L WITH STROKE]
                case '\u023D': // Ƚ  [LATIN CAPITAL LETTER L WITH BAR]
                case '\u029F': // ʟ  [LATIN LETTER SMALL CAPITAL L]
                case '\u1D0C': // ᴌ  [LATIN LETTER SMALL CAPITAL L WITH STROKE]
                case '\u1E36': // Ḷ  [LATIN CAPITAL LETTER L WITH DOT BELOW]
                case '\u1E38': // Ḹ  [LATIN CAPITAL LETTER L WITH DOT BELOW AND MACRON]
                case '\u1E3A': // Ḻ  [LATIN CAPITAL LETTER L WITH LINE BELOW]
                case '\u1E3C': // Ḽ  [LATIN CAPITAL LETTER L WITH CIRCUMFLEX BELOW]
                case '\u24C1': // Ⓛ  [CIRCLED LATIN CAPITAL LETTER L]
                case '\u2C60': // Ⱡ  [LATIN CAPITAL LETTER L WITH DOUBLE BAR]
                case '\u2C62': // Ɫ  [LATIN CAPITAL LETTER L WITH MIDDLE TILDE]
                case '\uA746': // Ꝇ  [LATIN CAPITAL LETTER BROKEN L]
                case '\uA748': // Ꝉ  [LATIN CAPITAL LETTER L WITH HIGH STROKE]
                case '\uA780': // Ꞁ  [LATIN CAPITAL LETTER TURNED L]
                case '\uFF2C': // Ｌ  [FULLWIDTH LATIN CAPITAL LETTER L]
                case '\u039B': // Λ  [GREEK CAPITAL LETTER LAMDA]
                case '\u1D27': // ᴧ  [GREEK LETTER SMALL CAPITAL LAMDA]
                case '\u2C96': // Ⲗ  [COPTIC CAPITAL LETTER LAULA]
                case '\u041B': // Л  [CYRILLIC CAPITAL LETTER EL]
                case '\u0409': // Љ  [CYRILLIC CAPITAL LETTER LJE]
                case '\u2C0E': // Ⰾ  [GLAGOLITIC CAPITAL LETTER LJUDIJE]
                case '\u16DA': // ᛚ  [RUNIC LETTER LAUKAZ LAGU LOGR L]
                case '\u16DB': // ᛛ  [RUNIC LETTER DOTTED-L]
                    return "L";
                case '\u013A': // ĺ  [LATIN SMALL LETTER L WITH ACUTE]
                case '\u013C': // ļ  [LATIN SMALL LETTER L WITH CEDILLA]
                case '\u013E': // ľ  [LATIN SMALL LETTER L WITH CARON]
                case '\u0140': // ŀ  [LATIN SMALL LETTER L WITH MIDDLE DOT]
                case '\u0142': // ł  [LATIN SMALL LETTER L WITH STROKE]
                case '\u019A': // ƚ  [LATIN SMALL LETTER L WITH BAR]
                case '\u0234': // ȴ  [LATIN SMALL LETTER L WITH CURL]
                case '\u026B': // ɫ  [LATIN SMALL LETTER L WITH MIDDLE TILDE]
                case '\u026C': // ɬ  [LATIN SMALL LETTER L WITH BELT]
                case '\u026D': // ɭ  [LATIN SMALL LETTER L WITH RETROFLEX HOOK]
                case '\u1D85': // ᶅ  [LATIN SMALL LETTER L WITH PALATAL HOOK]
                case '\u1E37': // ḷ  [LATIN SMALL LETTER L WITH DOT BELOW]
                case '\u1E39': // ḹ  [LATIN SMALL LETTER L WITH DOT BELOW AND MACRON]
                case '\u1E3B': // ḻ  [LATIN SMALL LETTER L WITH LINE BELOW]
                case '\u1E3D': // ḽ  [LATIN SMALL LETTER L WITH CIRCUMFLEX BELOW]
                case '\u24DB': // ⓛ  [CIRCLED LATIN SMALL LETTER L]
                case '\u2C61': // ⱡ  [LATIN SMALL LETTER L WITH DOUBLE BAR]
                case '\uA747': // ꝇ  [LATIN SMALL LETTER BROKEN L]
                case '\uA749': // ꝉ  [LATIN SMALL LETTER L WITH HIGH STROKE]
                case '\uA781': // ꞁ  [LATIN SMALL LETTER TURNED L]
                case '\uFF4C': // ｌ  [FULLWIDTH LATIN SMALL LETTER L]
                case '\u03BB': // λ  [GREEK SMALL LETTER LAMDA]
                case '\u2C97': // ⲗ  [COPTIC SMALL LETTER LAULA]
                case '\u043B': // л  [CYRILLIC SMALL LETTER EL]
                case '\u0459': // љ  [CYRILLIC SMALL LETTER LJE]
                case '\u2C3E': // ⰾ  [GLAGOLITIC SMALL LETTER LJUDIJE]
                    return "l";
                case '\u01C7': // Ǉ  [LATIN CAPITAL LETTER LJ]
                    return "LJ";
                case '\u1EFA': // Ỻ  [LATIN CAPITAL LETTER MIDDLE-WELSH LL]
                    return "LL";
                case '\u01C8': // ǈ  [LATIN CAPITAL LETTER L WITH SMALL LETTER J]
                    return "Lj";
                case '\u24A7': // ⒧  [PARENTHESIZED LATIN SMALL LETTER L]
                    return "(l)";
                case '\u01C9': // ǉ  [LATIN SMALL LETTER LJ]
                    return "lj";
                case '\u1EFB': // ỻ  [LATIN SMALL LETTER MIDDLE-WELSH LL]
                    return "ll";
                case '\u02AA': // ʪ  [LATIN SMALL LETTER LS DIGRAPH]
                    return "ls";
                case '\u02AB': // ʫ  [LATIN SMALL LETTER LZ DIGRAPH]
                    return "lz";
                case '\u019C': // Ɯ  [LATIN CAPITAL LETTER TURNED M]
                case '\u1D0D': // ᴍ  [LATIN LETTER SMALL CAPITAL M]
                case '\u1E3E': // Ḿ  [LATIN CAPITAL LETTER M WITH ACUTE]
                case '\u1E40': // Ṁ  [LATIN CAPITAL LETTER M WITH DOT ABOVE]
                case '\u1E42': // Ṃ  [LATIN CAPITAL LETTER M WITH DOT BELOW]
                case '\u24C2': // Ⓜ  [CIRCLED LATIN CAPITAL LETTER M]
                case '\u2C6E': // Ɱ  [LATIN CAPITAL LETTER M WITH HOOK]
                case '\uA7FD': // ꟽ  [LATIN EPIGRAPHIC LETTER INVERTED M]
                case '\uA7FF': // ꟿ  [LATIN EPIGRAPHIC LETTER ARCHAIC M]
                case '\uFF2D': // Ｍ  [FULLWIDTH LATIN CAPITAL LETTER M]
                case '\u039C': // Μ  [GREEK CAPITAL LETTER MU]
                case '\u2C98': // Ⲙ  [COPTIC CAPITAL LETTER MI]
                case '\u041C': // М  [CYRILLIC CAPITAL LETTER EM]
                case '\u2C0F': // Ⰿ  [GLAGOLITIC CAPITAL LETTER MYSLITE]
                case '\u2C2E': // Ⱞ  [GLAGOLITIC CAPITAL LETTER LATINATE MYSLITE]
                case '\u16D7': // ᛗ  [RUNIC LETTER MANNAZ MAN M]
                case '\u16D8': // ᛘ  [RUNIC LETTER LONG-BRANCH-MADR M]
                case '\u16D9': // ᛙ  [RUNIC LETTER SHORT-TWIG-MADR M]
                    return "M";
                case '\u026F': // ɯ  [LATIN SMALL LETTER TURNED M]
                case '\u0270': // ɰ  [LATIN SMALL LETTER TURNED M WITH LONG LEG]
                case '\u0271': // ɱ  [LATIN SMALL LETTER M WITH HOOK]
                case '\u1D6F': // ᵯ  [LATIN SMALL LETTER M WITH MIDDLE TILDE]
                case '\u1D86': // ᶆ  [LATIN SMALL LETTER M WITH PALATAL HOOK]
                case '\u1E3F': // ḿ  [LATIN SMALL LETTER M WITH ACUTE]
                case '\u1E41': // ṁ  [LATIN SMALL LETTER M WITH DOT ABOVE]
                case '\u1E43': // ṃ  [LATIN SMALL LETTER M WITH DOT BELOW]
                case '\u24DC': // ⓜ  [CIRCLED LATIN SMALL LETTER M]
                case '\uFF4D': // ｍ  [FULLWIDTH LATIN SMALL LETTER M]
                case '\u03BC': // μ  [GREEK SMALL LETTER MU]
                case '\u00B5': // µ  [MICRO SIGN]
                case '\u2C99': // ⲙ  [COPTIC SMALL LETTER MI]
                case '\u043C': // м  [CYRILLIC SMALL LETTER EM]
                case '\u2C3F': // ⰿ  [GLAGOLITIC SMALL LETTER MYSLITE]
                case '\u2C5E': // ⱞ  [GLAGOLITIC SMALL LETTER LATINATE MYSLITE]
                    return "m";
                case '\u24A8': // ⒨  [PARENTHESIZED LATIN SMALL LETTER M]
                    return "(m)";
                case '\u00D1': // Ñ  [LATIN CAPITAL LETTER N WITH TILDE]
                case '\u0143': // Ń  [LATIN CAPITAL LETTER N WITH ACUTE]
                case '\u0145': // Ņ  [LATIN CAPITAL LETTER N WITH CEDILLA]
                case '\u0147': // Ň  [LATIN CAPITAL LETTER N WITH CARON]
                case '\u014A': // Ŋ  [LATIN CAPITAL LETTER ENG]
                case '\u019D': // Ɲ  [LATIN CAPITAL LETTER N WITH LEFT HOOK]
                case '\u01F8': // Ǹ  [LATIN CAPITAL LETTER N WITH GRAVE]
                case '\u0220': // Ƞ  [LATIN CAPITAL LETTER N WITH LONG RIGHT LEG]
                case '\u0274': // ɴ  [LATIN LETTER SMALL CAPITAL N]
                case '\u1D0E': // ᴎ  [LATIN LETTER SMALL CAPITAL REVERSED N]
                case '\u1E44': // Ṅ  [LATIN CAPITAL LETTER N WITH DOT ABOVE]
                case '\u1E46': // Ṇ  [LATIN CAPITAL LETTER N WITH DOT BELOW]
                case '\u1E48': // Ṉ  [LATIN CAPITAL LETTER N WITH LINE BELOW]
                case '\u1E4A': // Ṋ  [LATIN CAPITAL LETTER N WITH CIRCUMFLEX BELOW]
                case '\u24C3': // Ⓝ  [CIRCLED LATIN CAPITAL LETTER N]
                case '\uFF2E': // Ｎ  [FULLWIDTH LATIN CAPITAL LETTER N]
                case '\u039D': // Ν  [GREEK CAPITAL LETTER NU]
                case '\u2C9A': // Ⲛ  [COPTIC CAPITAL LETTER NI]
                case '\u041D': // Н  [CYRILLIC CAPITAL LETTER EN]
                case '\u040A': // Њ  [CYRILLIC CAPITAL LETTER NJE]
                case '\u2C10': // Ⱀ  [GLAGOLITIC CAPITAL LETTER NASHI]
                case '\u16BE': // ᚾ  [RUNIC LETTER NAUDIZ NYD NAUD N]
                case '\u16DC': // ᛜ  [RUNIC LETTER INGWAZ]
                case '\u16DD': // ᛝ  [RUNIC LETTER ING]
                case '\u16BF': // ᚿ  [RUNIC LETTER SHORT-TWIG-NAUD N]
                case '\u16C0': // ᛀ  [RUNIC LETTER DOTTED-N]
                    return "N";
                case '\u00F1': // ñ  [LATIN SMALL LETTER N WITH TILDE]
                case '\u0144': // ń  [LATIN SMALL LETTER N WITH ACUTE]
                case '\u0146': // ņ  [LATIN SMALL LETTER N WITH CEDILLA]
                case '\u0148': // ň  [LATIN SMALL LETTER N WITH CARON]
                case '\u0149': // ŉ  [LATIN SMALL LETTER N PRECEDED BY APOSTROPHE]
                case '\u014B': // ŋ  [LATIN SMALL LETTER ENG]
                case '\u019E': // ƞ  [LATIN SMALL LETTER N WITH LONG RIGHT LEG]
                case '\u01F9': // ǹ  [LATIN SMALL LETTER N WITH GRAVE]
                case '\u0235': // ȵ  [LATIN SMALL LETTER N WITH CURL]
                case '\u0272': // ɲ  [LATIN SMALL LETTER N WITH LEFT HOOK]
                case '\u0273': // ɳ  [LATIN SMALL LETTER N WITH RETROFLEX HOOK]
                case '\u1D70': // ᵰ  [LATIN SMALL LETTER N WITH MIDDLE TILDE]
                case '\u1D87': // ᶇ  [LATIN SMALL LETTER N WITH PALATAL HOOK]
                case '\u1E45': // ṅ  [LATIN SMALL LETTER N WITH DOT ABOVE]
                case '\u1E47': // ṇ  [LATIN SMALL LETTER N WITH DOT BELOW]
                case '\u1E49': // ṉ  [LATIN SMALL LETTER N WITH LINE BELOW]
                case '\u1E4B': // ṋ  [LATIN SMALL LETTER N WITH CIRCUMFLEX BELOW]
                case '\u207F': // ⁿ  [SUPERSCRIPT LATIN SMALL LETTER N]
                case '\u24DD': // ⓝ  [CIRCLED LATIN SMALL LETTER N]
                case '\uFF4E': // ｎ  [FULLWIDTH LATIN SMALL LETTER N]
                case '\u03BD': // ν  [GREEK SMALL LETTER NU]
                case '\u2C9B': // ⲛ  [COPTIC SMALL LETTER NI]
                case '\u043D': // н  [CYRILLIC SMALL LETTER EN]
                case '\u045A': // њ  [CYRILLIC SMALL LETTER NJE]
                case '\u2C40': // ⱀ  [GLAGOLITIC SMALL LETTER NASHI]
                    return "n";
                case '\u01CA': // Ǌ  [LATIN CAPITAL LETTER NJ]
                    return "NJ";
                case '\u01CB': // ǋ  [LATIN CAPITAL LETTER N WITH SMALL LETTER J]
                    return "Nj";
                case '\u24A9': // ⒩  [PARENTHESIZED LATIN SMALL LETTER N]
                    return "(n)";
                case '\u01CC': // ǌ  [LATIN SMALL LETTER NJ]
                    return "nj";
                case '\u00D2': // Ò  [LATIN CAPITAL LETTER O WITH GRAVE]
                case '\u00D3': // Ó  [LATIN CAPITAL LETTER O WITH ACUTE]
                case '\u00D4': // Ô  [LATIN CAPITAL LETTER O WITH CIRCUMFLEX]
                case '\u00D5': // Õ  [LATIN CAPITAL LETTER O WITH TILDE]
                case '\u00D6': // Ö  [LATIN CAPITAL LETTER O WITH DIAERESIS]
                case '\u00D8': // Ø  [LATIN CAPITAL LETTER O WITH STROKE]
                case '\u014C': // Ō  [LATIN CAPITAL LETTER O WITH MACRON]
                case '\u014E': // Ŏ  [LATIN CAPITAL LETTER O WITH BREVE]
                case '\u0150': // Ő  [LATIN CAPITAL LETTER O WITH DOUBLE ACUTE]
                case '\u0186': // Ɔ  [LATIN CAPITAL LETTER OPEN O]
                case '\u019F': // Ɵ  [LATIN CAPITAL LETTER O WITH MIDDLE TILDE]
                case '\u01A0': // Ơ  [LATIN CAPITAL LETTER O WITH HORN]
                case '\u01D1': // Ǒ  [LATIN CAPITAL LETTER O WITH CARON]
                case '\u01EA': // Ǫ  [LATIN CAPITAL LETTER O WITH OGONEK]
                case '\u01EC': // Ǭ  [LATIN CAPITAL LETTER O WITH OGONEK AND MACRON]
                case '\u01FE': // Ǿ  [LATIN CAPITAL LETTER O WITH STROKE AND ACUTE]
                case '\u020C': // Ȍ  [LATIN CAPITAL LETTER O WITH DOUBLE GRAVE]
                case '\u020E': // Ȏ  [LATIN CAPITAL LETTER O WITH INVERTED BREVE]
                case '\u022A': // Ȫ  [LATIN CAPITAL LETTER O WITH DIAERESIS AND MACRON]
                case '\u022C': // Ȭ  [LATIN CAPITAL LETTER O WITH TILDE AND MACRON]
                case '\u022E': // Ȯ  [LATIN CAPITAL LETTER O WITH DOT ABOVE]
                case '\u0230': // Ȱ  [LATIN CAPITAL LETTER O WITH DOT ABOVE AND MACRON]
                case '\u1D0F': // ᴏ  [LATIN LETTER SMALL CAPITAL O]
                case '\u1D10': // ᴐ  [LATIN LETTER SMALL CAPITAL OPEN O]
                case '\u1E4C': // Ṍ  [LATIN CAPITAL LETTER O WITH TILDE AND ACUTE]
                case '\u1E4E': // Ṏ  [LATIN CAPITAL LETTER O WITH TILDE AND DIAERESIS]
                case '\u1E50': // Ṑ  [LATIN CAPITAL LETTER O WITH MACRON AND GRAVE]
                case '\u1E52': // Ṓ  [LATIN CAPITAL LETTER O WITH MACRON AND ACUTE]
                case '\u1ECC': // Ọ  [LATIN CAPITAL LETTER O WITH DOT BELOW]
                case '\u1ECE': // Ỏ  [LATIN CAPITAL LETTER O WITH HOOK ABOVE]
                case '\u1ED0': // Ố  [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND ACUTE]
                case '\u1ED2': // Ồ  [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND GRAVE]
                case '\u1ED4': // Ổ  [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE]
                case '\u1ED6': // Ỗ  [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND TILDE]
                case '\u1ED8': // Ộ  [LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND DOT BELOW]
                case '\u1EDA': // Ớ  [LATIN CAPITAL LETTER O WITH HORN AND ACUTE]
                case '\u1EDC': // Ờ  [LATIN CAPITAL LETTER O WITH HORN AND GRAVE]
                case '\u1EDE': // Ở  [LATIN CAPITAL LETTER O WITH HORN AND HOOK ABOVE]
                case '\u1EE0': // Ỡ  [LATIN CAPITAL LETTER O WITH HORN AND TILDE]
                case '\u1EE2': // Ợ  [LATIN CAPITAL LETTER O WITH HORN AND DOT BELOW]
                case '\u24C4': // Ⓞ  [CIRCLED LATIN CAPITAL LETTER O]
                case '\uA74A': // Ꝋ  [LATIN CAPITAL LETTER O WITH LONG STROKE OVERLAY]
                case '\uA74C': // Ꝍ  [LATIN CAPITAL LETTER O WITH LOOP]
                case '\uFF2F': // Ｏ  [FULLWIDTH LATIN CAPITAL LETTER O]
                case '\u039F': // Ο  [GREEK CAPITAL LETTER OMICRON]
                case '\u038C': // Ό  [GREEK CAPITAL LETTER OMICRON WITH TONOS]
                case '\u2C9E': // Ⲟ  [COPTIC CAPITAL LETTER O]
                case '\u03A9': // Ω  [GREEK CAPITAL LETTER OMEGA]
                case '\u038F': // Ώ  [GREEK CAPITAL LETTER OMEGA WITH TONOS]
                case '\uA7B6': // Ꞷ  [LATIN CAPITAL LETTER OMEGA]
                case '\u041E': // О  [CYRILLIC CAPITAL LETTER O]
                case '\u046A': // Ѫ  [CYRILLIC CAPITAL LETTER BIG YUS]
                case '\uA65A': // Ꙛ  [CYRILLIC CAPITAL LETTER BLENDED YUS]
                case '\u0460': // Ѡ  [CYRILLIC CAPITAL LETTER OMEGA]
                case '\u047C': // Ѽ  [CYRILLIC CAPITAL LETTER OMEGA WITH TITLO]
                case '\uA64C': // Ꙍ  [CYRILLIC CAPITAL LETTER BROAD OMEGA]
                case '\u047A': // Ѻ  [CYRILLIC CAPITAL LETTER ROUND OMEGA]
                case '\u2C11': // Ⱁ  [GLAGOLITIC CAPITAL LETTER ONU]
                case '\u2C19': // Ⱉ  [GLAGOLITIC CAPITAL LETTER OTU]
                case '\u2C28': // Ⱘ  [GLAGOLITIC CAPITAL LETTER BIG YUS]
                case '\u16DF': // ᛟ  [RUNIC LETTER OTHALAN ETHEL O]
                case '\u16AE': // ᚮ  [RUNIC LETTER O]
                case '\u16A9': // ᚩ  [RUNIC LETTER OS O]
                case '\u16B0': // ᚰ  [RUNIC LETTER ON]
                    return "O";
                case '\u00F2': // ò  [LATIN SMALL LETTER O WITH GRAVE]
                case '\u00F3': // ó  [LATIN SMALL LETTER O WITH ACUTE]
                case '\u00F4': // ô  [LATIN SMALL LETTER O WITH CIRCUMFLEX]
                case '\u00F5': // õ  [LATIN SMALL LETTER O WITH TILDE]
                case '\u00F6': // ö  [LATIN SMALL LETTER O WITH DIAERESIS]
                case '\u00F8': // ø  [LATIN SMALL LETTER O WITH STROKE]
                case '\u014D': // ō  [LATIN SMALL LETTER O WITH MACRON]
                case '\u014F': // ŏ  [LATIN SMALL LETTER O WITH BREVE]
                case '\u0151': // ő  [LATIN SMALL LETTER O WITH DOUBLE ACUTE]
                case '\u01A1': // ơ  [LATIN SMALL LETTER O WITH HORN]
                case '\u01D2': // ǒ  [LATIN SMALL LETTER O WITH CARON]
                case '\u01EB': // ǫ  [LATIN SMALL LETTER O WITH OGONEK]
                case '\u01ED': // ǭ  [LATIN SMALL LETTER O WITH OGONEK AND MACRON]
                case '\u01FF': // ǿ  [LATIN SMALL LETTER O WITH STROKE AND ACUTE]
                case '\u020D': // ȍ  [LATIN SMALL LETTER O WITH DOUBLE GRAVE]
                case '\u020F': // ȏ  [LATIN SMALL LETTER O WITH INVERTED BREVE]
                case '\u022B': // ȫ  [LATIN SMALL LETTER O WITH DIAERESIS AND MACRON]
                case '\u022D': // ȭ  [LATIN SMALL LETTER O WITH TILDE AND MACRON]
                case '\u022F': // ȯ  [LATIN SMALL LETTER O WITH DOT ABOVE]
                case '\u0231': // ȱ  [LATIN SMALL LETTER O WITH DOT ABOVE AND MACRON]
                case '\u0254': // ɔ  [LATIN SMALL LETTER OPEN O]
                case '\u0275': // ɵ  [LATIN SMALL LETTER BARRED O]
                case '\u1D16': // ᴖ  [LATIN SMALL LETTER TOP HALF O]
                case '\u1D17': // ᴗ  [LATIN SMALL LETTER BOTTOM HALF O]
                case '\u1D97': // ᶗ  [LATIN SMALL LETTER OPEN O WITH RETROFLEX HOOK]
                case '\u1E4D': // ṍ  [LATIN SMALL LETTER O WITH TILDE AND ACUTE]
                case '\u1E4F': // ṏ  [LATIN SMALL LETTER O WITH TILDE AND DIAERESIS]
                case '\u1E51': // ṑ  [LATIN SMALL LETTER O WITH MACRON AND GRAVE]
                case '\u1E53': // ṓ  [LATIN SMALL LETTER O WITH MACRON AND ACUTE]
                case '\u1ECD': // ọ  [LATIN SMALL LETTER O WITH DOT BELOW]
                case '\u1ECF': // ỏ  [LATIN SMALL LETTER O WITH HOOK ABOVE]
                case '\u1ED1': // ố  [LATIN SMALL LETTER O WITH CIRCUMFLEX AND ACUTE]
                case '\u1ED3': // ồ  [LATIN SMALL LETTER O WITH CIRCUMFLEX AND GRAVE]
                case '\u1ED5': // ổ  [LATIN SMALL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE]
                case '\u1ED7': // ỗ  [LATIN SMALL LETTER O WITH CIRCUMFLEX AND TILDE]
                case '\u1ED9': // ộ  [LATIN SMALL LETTER O WITH CIRCUMFLEX AND DOT BELOW]
                case '\u1EDB': // ớ  [LATIN SMALL LETTER O WITH HORN AND ACUTE]
                case '\u1EDD': // ờ  [LATIN SMALL LETTER O WITH HORN AND GRAVE]
                case '\u1EDF': // ở  [LATIN SMALL LETTER O WITH HORN AND HOOK ABOVE]
                case '\u1EE1': // ỡ  [LATIN SMALL LETTER O WITH HORN AND TILDE]
                case '\u1EE3': // ợ  [LATIN SMALL LETTER O WITH HORN AND DOT BELOW]
                case '\u2092': // ₒ  [LATIN SUBSCRIPT SMALL LETTER O]
                case '\u24DE': // ⓞ  [CIRCLED LATIN SMALL LETTER O]
                case '\u2C7A': // ⱺ  [LATIN SMALL LETTER O WITH LOW RING INSIDE]
                case '\uA74B': // ꝋ  [LATIN SMALL LETTER O WITH LONG STROKE OVERLAY]
                case '\uA74D': // ꝍ  [LATIN SMALL LETTER O WITH LOOP]
                case '\uFF4F': // ｏ  [FULLWIDTH LATIN SMALL LETTER O]
                case '\u00BA': // º  [MASCULINE ORDINAL INDICATOR]
                case '\u03BF': // ο  [GREEK SMALL LETTER OMICRON]
                case '\u03CC': // ό  [GREEK SMALL LETTER OMICRON WITH TONOS]
                case '\u2C9F': // ⲟ  [COPTIC SMALL LETTER O]
                case '\u03C9': // ω  [GREEK SMALL LETTER OMEGA]
                case '\u03CE': // ώ  [GREEK SMALL LETTER OMEGA WITH TONOS]
                case '\uA7B7': // ꞷ  [LATIN SMALL LETTER OMEGA]
                case '\u0277': // ɷ  [LATIN SMALL LETTER CLOSED OMEGA]
                case '\u043E': // о  [CYRILLIC SMALL LETTER O]
                case '\u046B': // ѫ  [CYRILLIC SMALL LETTER BIG YUS]
                case '\uA65B': // ꙛ  [CYRILLIC SMALL LETTER BLENDED YUS]
                case '\u0461': // ѡ  [CYRILLIC SMALL LETTER OMEGA]
                case '\u047D': // ѽ  [CYRILLIC SMALL LETTER OMEGA WITH TITLO]
                case '\uA64D': // ꙍ  [CYRILLIC SMALL LETTER BROAD OMEGA]
                case '\u047B': // ѻ  [CYRILLIC SMALL LETTER ROUND OMEGA]
                case '\u2C41': // ⱁ  [GLAGOLITIC SMALL LETTER ONU]
                case '\u2C49': // ⱉ  [GLAGOLITIC SMALL LETTER OTU]
                case '\u2C58': // ⱘ  [GLAGOLITIC SMALL LETTER BIG YUS]
                    return "o";
                case '\u0152': // Œ  [LATIN CAPITAL LIGATURE OE]
                case '\u0276': // ɶ  [LATIN LETTER SMALL CAPITAL OE]
                case '\u16AF': // ᚯ  [RUNIC LETTER OE]
                    return "OE";
                case '\uA74E': // Ꝏ  [LATIN CAPITAL LETTER OO]
                case '\u16F3': // ᛳ  [RUNIC LETTER OO]
                    return "OO";
                case '\u16F4': // ᛴ  [RUNIC LETTER FRANKS CASKET OS]
                    return "OS";
                case '\u0222': // Ȣ  [LATIN CAPITAL LETTER OU]
                case '\u1D15': // ᴕ  [LATIN LETTER SMALL CAPITAL OU]
                    return "OU";
                case '\u24AA': // ⒪  [PARENTHESIZED LATIN SMALL LETTER O]
                    return "(o)";
                case '\u0153': // œ  [LATIN SMALL LIGATURE OE]
                case '\u1D14': // ᴔ  [LATIN SMALL LETTER TURNED OE]
                    return "oe";
                case '\uA74F': // ꝏ  [LATIN SMALL LETTER OO]
                    return "oo";
                case '\u0223': // ȣ  [LATIN SMALL LETTER OU]
                    return "ou";
                case '\u01A4': // Ƥ  [LATIN CAPITAL LETTER P WITH HOOK]
                case '\u1D18': // ᴘ  [LATIN LETTER SMALL CAPITAL P]
                case '\u1E54': // Ṕ  [LATIN CAPITAL LETTER P WITH ACUTE]
                case '\u1E56': // Ṗ  [LATIN CAPITAL LETTER P WITH DOT ABOVE]
                case '\u24C5': // Ⓟ  [CIRCLED LATIN CAPITAL LETTER P]
                case '\u2C63': // Ᵽ  [LATIN CAPITAL LETTER P WITH STROKE]
                case '\uA750': // Ꝑ  [LATIN CAPITAL LETTER P WITH STROKE THROUGH DESCENDER]
                case '\uA752': // Ꝓ  [LATIN CAPITAL LETTER P WITH FLOURISH]
                case '\uA754': // Ꝕ  [LATIN CAPITAL LETTER P WITH SQUIRREL TAIL]
                case '\uFF30': // Ｐ  [FULLWIDTH LATIN CAPITAL LETTER P]
                case '\u03A0': // Π  [GREEK CAPITAL LETTER PI]
                case '\u2CA0': // Ⲡ  [COPTIC CAPITAL LETTER PI]
                case '\u041F': // П  [CYRILLIC CAPITAL LETTER PE]
                case '\u2C12': // Ⱂ  [GLAGOLITIC CAPITAL LETTER POKOJI]
                case '\u2C1A': // Ⱊ  [GLAGOLITIC CAPITAL LETTER PE]
                case '\u16C8': // ᛈ  [RUNIC LETTER PERTHO PEORTH P]
                case '\u16D4': // ᛔ  [RUNIC LETTER DOTTED-P]
                case '\u16D5': // ᛕ  [RUNIC LETTER OPEN-P]
                    return "P";
                case '\u01A5': // ƥ  [LATIN SMALL LETTER P WITH HOOK]
                case '\u1D71': // ᵱ  [LATIN SMALL LETTER P WITH MIDDLE TILDE]
                case '\u1D7D': // ᵽ  [LATIN SMALL LETTER P WITH STROKE]
                case '\u1D88': // ᶈ  [LATIN SMALL LETTER P WITH PALATAL HOOK]
                case '\u1E55': // ṕ  [LATIN SMALL LETTER P WITH ACUTE]
                case '\u1E57': // ṗ  [LATIN SMALL LETTER P WITH DOT ABOVE]
                case '\u24DF': // ⓟ  [CIRCLED LATIN SMALL LETTER P]
                case '\uA751': // ꝑ  [LATIN SMALL LETTER P WITH STROKE THROUGH DESCENDER]
                case '\uA753': // ꝓ  [LATIN SMALL LETTER P WITH FLOURISH]
                case '\uA755': // ꝕ  [LATIN SMALL LETTER P WITH SQUIRREL TAIL]
                case '\uA7FC': // ꟼ  [LATIN EPIGRAPHIC LETTER REVERSED P]
                case '\uFF50': // ｐ  [FULLWIDTH LATIN SMALL LETTER P]
                case '\u03C0': // π  [GREEK SMALL LETTER PI]
                case '\u03D6': // ϖ  [GREEK PI SYMBOL]
                case '\u2CA1': // ⲡ  [COPTIC SMALL LETTER PI]
                case '\u043F': // п  [CYRILLIC SMALL LETTER PE]
                case '\u2C42': // ⱂ  [GLAGOLITIC SMALL LETTER POKOJI]
                case '\u2C4A': // ⱊ  [GLAGOLITIC SMALL LETTER PE]
                    return "p";
                case '\u24AB': // ⒫  [PARENTHESIZED LATIN SMALL LETTER P]
                    return "(p)";
                case '\u024A': // Ɋ  [LATIN CAPITAL LETTER SMALL Q WITH HOOK TAIL]
                case '\u24C6': // Ⓠ  [CIRCLED LATIN CAPITAL LETTER Q]
                case '\uA756': // Ꝗ  [LATIN CAPITAL LETTER Q WITH STROKE THROUGH DESCENDER]
                case '\uA758': // Ꝙ  [LATIN CAPITAL LETTER Q WITH DIAGONAL STROKE]
                case '\uFF31': // Ｑ  [FULLWIDTH LATIN CAPITAL LETTER Q]
                case '\u03D8': // Ϙ  [GREEK LETTER ARCHAIC KOPPA]
                case '\u03DE': // Ϟ  [GREEK LETTER KOPPA]
                case '\u16E9': // ᛩ  [RUNIC LETTER Q]
                    return "Q";
                case '\u0138': // ĸ  [LATIN SMALL LETTER KRA]
                case '\u024B': // ɋ  [LATIN SMALL LETTER Q WITH HOOK TAIL]
                case '\u02A0': // ʠ  [LATIN SMALL LETTER Q WITH HOOK]
                case '\u24E0': // ⓠ  [CIRCLED LATIN SMALL LETTER Q]
                case '\uA757': // ꝗ  [LATIN SMALL LETTER Q WITH STROKE THROUGH DESCENDER]
                case '\uA759': // ꝙ  [LATIN SMALL LETTER Q WITH DIAGONAL STROKE]
                case '\uFF51': // ｑ  [FULLWIDTH LATIN SMALL LETTER Q]
                case '\u03D9': // ϙ  [GREEK SMALL LETTER ARCHAIC KOPPA]
                case '\u03DF': // ϟ  [GREEK SMALL LETTER KOPPA]
                    return "q";
                case '\u24AC': // ⒬  [PARENTHESIZED LATIN SMALL LETTER Q]
                    return "(q)";
                case '\u0239': // ȹ  [LATIN SMALL LETTER QP DIGRAPH]
                    return "qp";
                case '\u0154': // Ŕ  [LATIN CAPITAL LETTER R WITH ACUTE]
                case '\u0156': // Ŗ  [LATIN CAPITAL LETTER R WITH CEDILLA]
                case '\u0158': // Ř  [LATIN CAPITAL LETTER R WITH CARON]
                case '\u0210': // Ȑ  [LATIN CAPITAL LETTER R WITH DOUBLE GRAVE]
                case '\u0212': // Ȓ  [LATIN CAPITAL LETTER R WITH INVERTED BREVE]
                case '\u024C': // Ɍ  [LATIN CAPITAL LETTER R WITH STROKE]
                case '\u0280': // ʀ  [LATIN LETTER SMALL CAPITAL R]
                case '\u0281': // ʁ  [LATIN LETTER SMALL CAPITAL INVERTED R]
                case '\u1D19': // ᴙ  [LATIN LETTER SMALL CAPITAL REVERSED R]
                case '\u1D1A': // ᴚ  [LATIN LETTER SMALL CAPITAL TURNED R]
                case '\u1E58': // Ṙ  [LATIN CAPITAL LETTER R WITH DOT ABOVE]
                case '\u1E5A': // Ṛ  [LATIN CAPITAL LETTER R WITH DOT BELOW]
                case '\u1E5C': // Ṝ  [LATIN CAPITAL LETTER R WITH DOT BELOW AND MACRON]
                case '\u1E5E': // Ṟ  [LATIN CAPITAL LETTER R WITH LINE BELOW]
                case '\u24C7': // Ⓡ  [CIRCLED LATIN CAPITAL LETTER R]
                case '\u2C64': // Ɽ  [LATIN CAPITAL LETTER R WITH TAIL]
                case '\uA75A': // Ꝛ  [LATIN CAPITAL LETTER R ROTUNDA]
                case '\uA782': // Ꞃ  [LATIN CAPITAL LETTER INSULAR R]
                case '\uFF32': // Ｒ  [FULLWIDTH LATIN CAPITAL LETTER R]
                case '\u03A1': // Ρ  [GREEK CAPITAL LETTER RHO]
                case '\u1FEC': // Ῥ  [GREEK CAPITAL LETTER RHO WITH DASIA]
                case '\u1D29': // ᴩ  [GREEK LETTER SMALL CAPITAL RHO]
                case '\u2CA2': // Ⲣ  [COPTIC CAPITAL LETTER RO]
                case '\u0420': // Р  [CYRILLIC CAPITAL LETTER ER]
                case '\u2C13': // Ⱃ  [GLAGOLITIC CAPITAL LETTER RITSI]
                case '\u16B1': // ᚱ  [RUNIC LETTER RAIDO RAD REID R]
                case '\u16E6': // ᛦ  [RUNIC LETTER LONG-BRANCH-YR]
                case '\u16E7': // ᛧ  [RUNIC LETTER SHORT-TWIG-YR]
                case '\u16E8': // ᛨ  [RUNIC LETTER ICELANDIC-YR]
                    return "R";
                case '\u0155': // ŕ  [LATIN SMALL LETTER R WITH ACUTE]
                case '\u0157': // ŗ  [LATIN SMALL LETTER R WITH CEDILLA]
                case '\u0159': // ř  [LATIN SMALL LETTER R WITH CARON]
                case '\u0211': // ȑ  [LATIN SMALL LETTER R WITH DOUBLE GRAVE]
                case '\u0213': // ȓ  [LATIN SMALL LETTER R WITH INVERTED BREVE]
                case '\u024D': // ɍ  [LATIN SMALL LETTER R WITH STROKE]
                case '\u027C': // ɼ  [LATIN SMALL LETTER R WITH LONG LEG]
                case '\u027D': // ɽ  [LATIN SMALL LETTER R WITH TAIL]
                case '\u027E': // ɾ  [LATIN SMALL LETTER R WITH FISHHOOK]
                case '\u027F': // ɿ  [LATIN SMALL LETTER REVERSED R WITH FISHHOOK]
                case '\u1D63': // ᵣ  [LATIN SUBSCRIPT SMALL LETTER R]
                case '\u1D72': // ᵲ  [LATIN SMALL LETTER R WITH MIDDLE TILDE]
                case '\u1D73': // ᵳ  [LATIN SMALL LETTER R WITH FISHHOOK AND MIDDLE TILDE]
                case '\u1D89': // ᶉ  [LATIN SMALL LETTER R WITH PALATAL HOOK]
                case '\u1E59': // ṙ  [LATIN SMALL LETTER R WITH DOT ABOVE]
                case '\u1E5B': // ṛ  [LATIN SMALL LETTER R WITH DOT BELOW]
                case '\u1E5D': // ṝ  [LATIN SMALL LETTER R WITH DOT BELOW AND MACRON]
                case '\u1E5F': // ṟ  [LATIN SMALL LETTER R WITH LINE BELOW]
                case '\u24E1': // ⓡ  [CIRCLED LATIN SMALL LETTER R]
                case '\uA75B': // ꝛ  [LATIN SMALL LETTER R ROTUNDA]
                case '\uA783': // ꞃ  [LATIN SMALL LETTER INSULAR R]
                case '\uFF52': // ｒ  [FULLWIDTH LATIN SMALL LETTER R]
                case '\u03C1': // ρ  [GREEK SMALL LETTER RHO]
                case '\u1FE4': // ῤ  [GREEK SMALL LETTER RHO WITH PSILI]
                case '\u1FE5': // ῥ  [GREEK SMALL LETTER RHO WITH DASIA]
                case '\u03F1': // ϱ  [GREEK RHO SYMBOL]
                case '\u03FC': // ϼ  [GREEK RHO WITH STROKE SYMBOL]
                case '\u1D68': // ᵨ  [GREEK SUBSCRIPT SMALL LETTER RHO]
                case '\u2CA3': // ⲣ  [COPTIC SMALL LETTER RO]
                case '\u0440': // р  [CYRILLIC SMALL LETTER ER]
                case '\u2C43': // ⱃ  [GLAGOLITIC SMALL LETTER RITSI]
                    return "r";
                case '\u24AD': // ⒭  [PARENTHESIZED LATIN SMALL LETTER R]
                    return "(r)";
                case '\u015A': // Ś  [LATIN CAPITAL LETTER S WITH ACUTE]
                case '\u015C': // Ŝ  [LATIN CAPITAL LETTER S WITH CIRCUMFLEX]
                case '\u015E': // Ş  [LATIN CAPITAL LETTER S WITH CEDILLA]
                case '\u0160': // Š  [LATIN CAPITAL LETTER S WITH CARON]
                case '\u0218': // Ș  [LATIN CAPITAL LETTER S WITH COMMA BELOW]
                case '\u1E60': // Ṡ  [LATIN CAPITAL LETTER S WITH DOT ABOVE]
                case '\u1E62': // Ṣ  [LATIN CAPITAL LETTER S WITH DOT BELOW]
                case '\u1E64': // Ṥ  [LATIN CAPITAL LETTER S WITH ACUTE AND DOT ABOVE]
                case '\u1E66': // Ṧ  [LATIN CAPITAL LETTER S WITH CARON AND DOT ABOVE]
                case '\u1E68': // Ṩ  [LATIN CAPITAL LETTER S WITH DOT BELOW AND DOT ABOVE]
                case '\u24C8': // Ⓢ  [CIRCLED LATIN CAPITAL LETTER S]
                case '\uA731': // ꜱ  [LATIN LETTER SMALL CAPITAL S]
                case '\uA785': // ꞅ  [LATIN SMALL LETTER INSULAR S]
                case '\uFF33': // Ｓ  [FULLWIDTH LATIN CAPITAL LETTER S]
                case '\u03A3': // Σ  [GREEK CAPITAL LETTER SIGMA]
                case '\u03F9': // Ϲ  [GREEK CAPITAL LUNATE SIGMA SYMBOL]
                case '\u0421': // С  [CYRILLIC CAPITAL LETTER ES]
                case '\u2C14': // Ⱄ  [GLAGOLITIC CAPITAL LETTER SLOVO]
                case '\u16CA': // ᛊ  [RUNIC LETTER SOWILO S]
                case '\u16CB': // ᛋ  [RUNIC LETTER SIGEL LONG-BRANCH-SOL S]
                case '\u16CC': // ᛌ  [RUNIC LETTER SHORT-TWIG-SOL S]
                    return "S";
                case '\u015B': // ś  [LATIN SMALL LETTER S WITH ACUTE]
                case '\u015D': // ŝ  [LATIN SMALL LETTER S WITH CIRCUMFLEX]
                case '\u015F': // ş  [LATIN SMALL LETTER S WITH CEDILLA]
                case '\u0161': // š  [LATIN SMALL LETTER S WITH CARON]
                case '\u017F': // ſ  [LATIN SMALL LETTER LONG S]
                case '\u0219': // ș  [LATIN SMALL LETTER S WITH COMMA BELOW]
                case '\u023F': // ȿ  [LATIN SMALL LETTER S WITH SWASH TAIL]
                case '\u0282': // ʂ  [LATIN SMALL LETTER S WITH HOOK]
                case '\u1D74': // ᵴ  [LATIN SMALL LETTER S WITH MIDDLE TILDE]
                case '\u1D8A': // ᶊ  [LATIN SMALL LETTER S WITH PALATAL HOOK]
                case '\u1E61': // ṡ  [LATIN SMALL LETTER S WITH DOT ABOVE]
                case '\u1E63': // ṣ  [LATIN SMALL LETTER S WITH DOT BELOW]
                case '\u1E65': // ṥ  [LATIN SMALL LETTER S WITH ACUTE AND DOT ABOVE]
                case '\u1E67': // ṧ  [LATIN SMALL LETTER S WITH CARON AND DOT ABOVE]
                case '\u1E69': // ṩ  [LATIN SMALL LETTER S WITH DOT BELOW AND DOT ABOVE]
                case '\u1E9C': // ẜ  [LATIN SMALL LETTER LONG S WITH DIAGONAL STROKE]
                case '\u1E9D': // ẝ  [LATIN SMALL LETTER LONG S WITH HIGH STROKE]
                case '\u24E2': // ⓢ  [CIRCLED LATIN SMALL LETTER S]
                case '\uA784': // Ꞅ  [LATIN CAPITAL LETTER INSULAR S]
                case '\uFF53': // ｓ  [FULLWIDTH LATIN SMALL LETTER S]
                case '\u03C3': // σ  [GREEK SMALL LETTER SIGMA]
                case '\u03C2': // ς  [GREEK SMALL LETTER FINAL SIGMA]
                case '\u03F2': // ϲ  [GREEK LUNATE SIGMA SYMBOL]
                case '\u0441': // с  [CYRILLIC SMALL LETTER ES]
                case '\u2C44': // ⱄ  [GLAGOLITIC SMALL LETTER SLOVO]
                    return "s";
                case '\u1E9E': // ẞ  [LATIN CAPITAL LETTER SHARP S]
                    return "SS";
                case '\u16E5': // ᛥ  [RUNIC LETTER STAN]
                    return "ST";
                case '\u24AE': // ⒮  [PARENTHESIZED LATIN SMALL LETTER S]
                    return "(s)";
                case '\u00DF': // ß  [LATIN SMALL LETTER SHARP S]
                    return "ss";
                case '\uFB06': // ﬆ  [LATIN SMALL LIGATURE ST]
                    return "st";
                case '\u0162': // Ţ  [LATIN CAPITAL LETTER T WITH CEDILLA]
                case '\u0164': // Ť  [LATIN CAPITAL LETTER T WITH CARON]
                case '\u0166': // Ŧ  [LATIN CAPITAL LETTER T WITH STROKE]
                case '\u01AC': // Ƭ  [LATIN CAPITAL LETTER T WITH HOOK]
                case '\u01AE': // Ʈ  [LATIN CAPITAL LETTER T WITH RETROFLEX HOOK]
                case '\u021A': // Ț  [LATIN CAPITAL LETTER T WITH COMMA BELOW]
                case '\u023E': // Ⱦ  [LATIN CAPITAL LETTER T WITH DIAGONAL STROKE]
                case '\u1D1B': // ᴛ  [LATIN LETTER SMALL CAPITAL T]
                case '\u1E6A': // Ṫ  [LATIN CAPITAL LETTER T WITH DOT ABOVE]
                case '\u1E6C': // Ṭ  [LATIN CAPITAL LETTER T WITH DOT BELOW]
                case '\u1E6E': // Ṯ  [LATIN CAPITAL LETTER T WITH LINE BELOW]
                case '\u1E70': // Ṱ  [LATIN CAPITAL LETTER T WITH CIRCUMFLEX BELOW]
                case '\u24C9': // Ⓣ  [CIRCLED LATIN CAPITAL LETTER T]
                case '\uA786': // Ꞇ  [LATIN CAPITAL LETTER INSULAR T]
                case '\uFF34': // Ｔ  [FULLWIDTH LATIN CAPITAL LETTER T]
                case '\u03A4': // Τ  [GREEK CAPITAL LETTER TAU]
                case '\u2CA6': // Ⲧ  [COPTIC CAPITAL LETTER TAU]
                case '\u0422': // Т  [CYRILLIC CAPITAL LETTER TE]
                case '\u2C15': // Ⱅ  [GLAGOLITIC CAPITAL LETTER TVRIDO]
                case '\u16CF': // ᛏ  [RUNIC LETTER TIWAZ TIR TYR T]
                case '\u16D0': // ᛐ  [RUNIC LETTER SHORT-TWIG-TYR T]
                    return "T";
                case '\u0163': // ţ  [LATIN SMALL LETTER T WITH CEDILLA]
                case '\u0165': // ť  [LATIN SMALL LETTER T WITH CARON]
                case '\u0167': // ŧ  [LATIN SMALL LETTER T WITH STROKE]
                case '\u01AB': // ƫ  [LATIN SMALL LETTER T WITH PALATAL HOOK]
                case '\u01AD': // ƭ  [LATIN SMALL LETTER T WITH HOOK]
                case '\u021B': // ț  [LATIN SMALL LETTER T WITH COMMA BELOW]
                case '\u0236': // ȶ  [LATIN SMALL LETTER T WITH CURL]
                case '\u0287': // ʇ  [LATIN SMALL LETTER TURNED T]
                case '\u0288': // ʈ  [LATIN SMALL LETTER T WITH RETROFLEX HOOK]
                case '\u1D75': // ᵵ  [LATIN SMALL LETTER T WITH MIDDLE TILDE]
                case '\u1E6B': // ṫ  [LATIN SMALL LETTER T WITH DOT ABOVE]
                case '\u1E6D': // ṭ  [LATIN SMALL LETTER T WITH DOT BELOW]
                case '\u1E6F': // ṯ  [LATIN SMALL LETTER T WITH LINE BELOW]
                case '\u1E71': // ṱ  [LATIN SMALL LETTER T WITH CIRCUMFLEX BELOW]
                case '\u1E97': // ẗ  [LATIN SMALL LETTER T WITH DIAERESIS]
                case '\u24E3': // ⓣ  [CIRCLED LATIN SMALL LETTER T]
                case '\u2C66': // ⱦ  [LATIN SMALL LETTER T WITH DIAGONAL STROKE]
                case '\uFF54': // ｔ  [FULLWIDTH LATIN SMALL LETTER T]
                case '\u03C4': // τ  [GREEK SMALL LETTER TAU]
                case '\u2CA7': // ⲧ  [COPTIC SMALL LETTER TAU]
                case '\u0442': // т  [CYRILLIC SMALL LETTER TE]
                case '\u1C84': // ᲄ  [CYRILLIC SMALL LETTER TALL TE]
                case '\u1C85': // ᲅ  [CYRILLIC SMALL LETTER THREE-LEGGED TE]
                case '\u2C45': // ⱅ  [GLAGOLITIC SMALL LETTER TVRIDO]
                    return "t";
                case '\u00DE': // Þ  [LATIN CAPITAL LETTER THORN]
                case '\uA766': // Ꝧ  [LATIN CAPITAL LETTER THORN WITH STROKE THROUGH DESCENDER]
                case '\u0398': // Θ  [GREEK CAPITAL LETTER THETA]
                case '\u03F4': // ϴ  [GREEK CAPITAL THETA SYMBOL]
                case '\u2C90': // Ⲑ  [COPTIC CAPITAL LETTER THETHE]
                case '\u0472': // Ѳ  [CYRILLIC CAPITAL LETTER FITA]
                case '\u2C2A': // Ⱚ  [GLAGOLITIC CAPITAL LETTER FITA]
                case '\u16A6': // ᚦ  [RUNIC LETTER THURISAZ THURS THORN]
                case '\u16A7': // ᚧ  [RUNIC LETTER ETH]
                    return "TH";
                case '\uA728': // Ꜩ  [LATIN CAPITAL LETTER TZ]
                    return "TZ";
                case '\u24AF': // ⒯  [PARENTHESIZED LATIN SMALL LETTER T]
                    return "(t)";
                case '\u02A8': // ʨ  [LATIN SMALL LETTER TC DIGRAPH WITH CURL]
                    return "tc";
                case '\u00FE': // þ  [LATIN SMALL LETTER THORN]
                case '\u1D7A': // ᵺ  [LATIN SMALL LETTER TH WITH STRIKETHROUGH]
                case '\uA767': // ꝧ  [LATIN SMALL LETTER THORN WITH STROKE THROUGH DESCENDER]
                case '\u03B8': // θ  [GREEK SMALL LETTER THETA]
                case '\u03D1': // ϑ  [GREEK THETA SYMBOL]
                case '\u1DBF': // ᶿ  [MODIFIER LETTER SMALL THETA]
                case '\u2C91': // ⲑ  [COPTIC SMALL LETTER THETHE]
                case '\u0473': // ѳ  [CYRILLIC SMALL LETTER FITA]
                case '\u2C5A': // ⱚ  [GLAGOLITIC SMALL LETTER FITA]
                    return "th";
                case '\u02A6': // ʦ  [LATIN SMALL LETTER TS DIGRAPH]
                case '\u0446': // ц  [CYRILLIC SMALL LETTER TSE]
                case '\u045B': // ћ  [CYRILLIC SMALL LETTER TSHE]
                case '\u2C4C': // ⱌ  [GLAGOLITIC SMALL LETTER TSI]
                    return "ts";
                case '\uA729': // ꜩ  [LATIN SMALL LETTER TZ]
                    return "tz";
                case '\u00D9': // Ù  [LATIN CAPITAL LETTER U WITH GRAVE]
                case '\u00DA': // Ú  [LATIN CAPITAL LETTER U WITH ACUTE]
                case '\u00DB': // Û  [LATIN CAPITAL LETTER U WITH CIRCUMFLEX]
                case '\u00DC': // Ü  [LATIN CAPITAL LETTER U WITH DIAERESIS]
                case '\u0168': // Ũ  [LATIN CAPITAL LETTER U WITH TILDE]
                case '\u016A': // Ū  [LATIN CAPITAL LETTER U WITH MACRON]
                case '\u016C': // Ŭ  [LATIN CAPITAL LETTER U WITH BREVE]
                case '\u016E': // Ů  [LATIN CAPITAL LETTER U WITH RING ABOVE]
                case '\u0170': // Ű  [LATIN CAPITAL LETTER U WITH DOUBLE ACUTE]
                case '\u0172': // Ų  [LATIN CAPITAL LETTER U WITH OGONEK]
                case '\u01AF': // Ư  [LATIN CAPITAL LETTER U WITH HORN]
                case '\u01D3': // Ǔ  [LATIN CAPITAL LETTER U WITH CARON]
                case '\u01D5': // Ǖ  [LATIN CAPITAL LETTER U WITH DIAERESIS AND MACRON]
                case '\u01D7': // Ǘ  [LATIN CAPITAL LETTER U WITH DIAERESIS AND ACUTE]
                case '\u01D9': // Ǚ  [LATIN CAPITAL LETTER U WITH DIAERESIS AND CARON]
                case '\u01DB': // Ǜ  [LATIN CAPITAL LETTER U WITH DIAERESIS AND GRAVE]
                case '\u0214': // Ȕ  [LATIN CAPITAL LETTER U WITH DOUBLE GRAVE]
                case '\u0216': // Ȗ  [LATIN CAPITAL LETTER U WITH INVERTED BREVE]
                case '\u0244': // Ʉ  [LATIN CAPITAL LETTER U BAR]
                case '\u1D1C': // ᴜ  [LATIN LETTER SMALL CAPITAL U]
                case '\u1D7E': // ᵾ  [LATIN SMALL CAPITAL LETTER U WITH STROKE]
                case '\u1E72': // Ṳ  [LATIN CAPITAL LETTER U WITH DIAERESIS BELOW]
                case '\u1E74': // Ṵ  [LATIN CAPITAL LETTER U WITH TILDE BELOW]
                case '\u1E76': // Ṷ  [LATIN CAPITAL LETTER U WITH CIRCUMFLEX BELOW]
                case '\u1E78': // Ṹ  [LATIN CAPITAL LETTER U WITH TILDE AND ACUTE]
                case '\u1E7A': // Ṻ  [LATIN CAPITAL LETTER U WITH MACRON AND DIAERESIS]
                case '\u1EE4': // Ụ  [LATIN CAPITAL LETTER U WITH DOT BELOW]
                case '\u1EE6': // Ủ  [LATIN CAPITAL LETTER U WITH HOOK ABOVE]
                case '\u1EE8': // Ứ  [LATIN CAPITAL LETTER U WITH HORN AND ACUTE]
                case '\u1EEA': // Ừ  [LATIN CAPITAL LETTER U WITH HORN AND GRAVE]
                case '\u1EEC': // Ử  [LATIN CAPITAL LETTER U WITH HORN AND HOOK ABOVE]
                case '\u1EEE': // Ữ  [LATIN CAPITAL LETTER U WITH HORN AND TILDE]
                case '\u1EF0': // Ự  [LATIN CAPITAL LETTER U WITH HORN AND DOT BELOW]
                case '\u24CA': // Ⓤ  [CIRCLED LATIN CAPITAL LETTER U]
                case '\uFF35': // Ｕ  [FULLWIDTH LATIN CAPITAL LETTER U]
                case '\u01B1': // Ʊ  [LATIN CAPITAL LETTER UPSILON]
                case '\u0423': // У  [CYRILLIC CAPITAL LETTER U]
                case '\u040E': // Ў  [CYRILLIC CAPITAL LETTER SHORT U]
                case '\u2CA8': // Ⲩ  [COPTIC CAPITAL LETTER UA]
                case '\u0478': // Ѹ  [CYRILLIC CAPITAL LETTER UK]
                case '\uA64A': // Ꙋ  [CYRILLIC CAPITAL LETTER MONOGRAPH UK]
                case '\u2C16': // Ⱆ  [GLAGOLITIC CAPITAL LETTER UKU]
                case '\u16A2': // ᚢ  [RUNIC LETTER URUZ UR U]
                    return "U";
                case '\u00F9': // ù  [LATIN SMALL LETTER U WITH GRAVE]
                case '\u00FA': // ú  [LATIN SMALL LETTER U WITH ACUTE]
                case '\u00FB': // û  [LATIN SMALL LETTER U WITH CIRCUMFLEX]
                case '\u00FC': // ü  [LATIN SMALL LETTER U WITH DIAERESIS]
                case '\u0169': // ũ  [LATIN SMALL LETTER U WITH TILDE]
                case '\u016B': // ū  [LATIN SMALL LETTER U WITH MACRON]
                case '\u016D': // ŭ  [LATIN SMALL LETTER U WITH BREVE]
                case '\u016F': // ů  [LATIN SMALL LETTER U WITH RING ABOVE]
                case '\u0171': // ű  [LATIN SMALL LETTER U WITH DOUBLE ACUTE]
                case '\u0173': // ų  [LATIN SMALL LETTER U WITH OGONEK]
                case '\u01B0': // ư  [LATIN SMALL LETTER U WITH HORN]
                case '\u01D4': // ǔ  [LATIN SMALL LETTER U WITH CARON]
                case '\u01D6': // ǖ  [LATIN SMALL LETTER U WITH DIAERESIS AND MACRON]
                case '\u01D8': // ǘ  [LATIN SMALL LETTER U WITH DIAERESIS AND ACUTE]
                case '\u01DA': // ǚ  [LATIN SMALL LETTER U WITH DIAERESIS AND CARON]
                case '\u01DC': // ǜ  [LATIN SMALL LETTER U WITH DIAERESIS AND GRAVE]
                case '\u0215': // ȕ  [LATIN SMALL LETTER U WITH DOUBLE GRAVE]
                case '\u0217': // ȗ  [LATIN SMALL LETTER U WITH INVERTED BREVE]
                case '\u0289': // ʉ  [LATIN SMALL LETTER U BAR]
                case '\u1D64': // ᵤ  [LATIN SUBSCRIPT SMALL LETTER U]
                case '\u1D99': // ᶙ  [LATIN SMALL LETTER U WITH RETROFLEX HOOK]
                case '\u1E73': // ṳ  [LATIN SMALL LETTER U WITH DIAERESIS BELOW]
                case '\u1E75': // ṵ  [LATIN SMALL LETTER U WITH TILDE BELOW]
                case '\u1E77': // ṷ  [LATIN SMALL LETTER U WITH CIRCUMFLEX BELOW]
                case '\u1E79': // ṹ  [LATIN SMALL LETTER U WITH TILDE AND ACUTE]
                case '\u1E7B': // ṻ  [LATIN SMALL LETTER U WITH MACRON AND DIAERESIS]
                case '\u1EE5': // ụ  [LATIN SMALL LETTER U WITH DOT BELOW]
                case '\u1EE7': // ủ  [LATIN SMALL LETTER U WITH HOOK ABOVE]
                case '\u1EE9': // ứ  [LATIN SMALL LETTER U WITH HORN AND ACUTE]
                case '\u1EEB': // ừ  [LATIN SMALL LETTER U WITH HORN AND GRAVE]
                case '\u1EED': // ử  [LATIN SMALL LETTER U WITH HORN AND HOOK ABOVE]
                case '\u1EEF': // ữ  [LATIN SMALL LETTER U WITH HORN AND TILDE]
                case '\u1EF1': // ự  [LATIN SMALL LETTER U WITH HORN AND DOT BELOW]
                case '\u24E4': // ⓤ  [CIRCLED LATIN SMALL LETTER U]
                case '\uFF55': // ｕ  [FULLWIDTH LATIN SMALL LETTER U]
                case '\u028A': // ʊ  [LATIN SMALL LETTER UPSILON]
                case '\u1DB7': // ᶷ  [MODIFIER LETTER SMALL UPSILON]
                case '\u1D7F': // ᵿ  [LATIN SMALL LETTER UPSILON WITH STROKE]
                case '\u0443': // у  [CYRILLIC SMALL LETTER U]
                case '\u045E': // ў  [CYRILLIC SMALL LETTER SHORT U]
                case '\u2CA9': // ⲩ  [COPTIC SMALL LETTER UA]
                case '\u0479': // ѹ  [CYRILLIC SMALL LETTER UK]
                case '\uA64B': // ꙋ  [CYRILLIC SMALL LETTER MONOGRAPH UK]
                case '\u2C46': // ⱆ  [GLAGOLITIC SMALL LETTER UKU]
                    return "u";
                case '\u24B0': // ⒰  [PARENTHESIZED LATIN SMALL LETTER U]
                    return "(u)";
                case '\u1D6B': // ᵫ  [LATIN SMALL LETTER UE]
                    return "ue";
                case '\u01B2': // Ʋ  [LATIN CAPITAL LETTER V WITH HOOK]
                case '\u0245': // Ʌ  [LATIN CAPITAL LETTER TURNED V]
                case '\u1D20': // ᴠ  [LATIN LETTER SMALL CAPITAL V]
                case '\u1E7C': // Ṽ  [LATIN CAPITAL LETTER V WITH TILDE]
                case '\u1E7E': // Ṿ  [LATIN CAPITAL LETTER V WITH DOT BELOW]
                case '\u1EFC': // Ỽ  [LATIN CAPITAL LETTER MIDDLE-WELSH V]
                case '\u24CB': // Ⓥ  [CIRCLED LATIN CAPITAL LETTER V]
                case '\uA75E': // Ꝟ  [LATIN CAPITAL LETTER V WITH DIAGONAL STROKE]
                case '\uA768': // Ꝩ  [LATIN CAPITAL LETTER VEND]
                case '\uFF36': // Ｖ  [FULLWIDTH LATIN CAPITAL LETTER V]
                case '\u0412': // В  [CYRILLIC CAPITAL LETTER VE]
                case '\u2C02': // Ⰲ  [GLAGOLITIC CAPITAL LETTER VEDE]
                case '\u16A1': // ᚡ  [RUNIC LETTER V]
                    return "V";
                case '\u028B': // ʋ  [LATIN SMALL LETTER V WITH HOOK]
                case '\u028C': // ʌ  [LATIN SMALL LETTER TURNED V]
                case '\u1D65': // ᵥ  [LATIN SUBSCRIPT SMALL LETTER V]
                case '\u1D8C': // ᶌ  [LATIN SMALL LETTER V WITH PALATAL HOOK]
                case '\u1E7D': // ṽ  [LATIN SMALL LETTER V WITH TILDE]
                case '\u1E7F': // ṿ  [LATIN SMALL LETTER V WITH DOT BELOW]
                case '\u24E5': // ⓥ  [CIRCLED LATIN SMALL LETTER V]
                case '\u2C71': // ⱱ  [LATIN SMALL LETTER V WITH RIGHT HOOK]
                case '\u2C74': // ⱴ  [LATIN SMALL LETTER V WITH CURL]
                case '\uA75F': // ꝟ  [LATIN SMALL LETTER V WITH DIAGONAL STROKE]
                case '\uFF56': // ｖ  [FULLWIDTH LATIN SMALL LETTER V]
                case '\u0432': // в  [CYRILLIC SMALL LETTER VE]
                case '\u1C80': // ᲀ  [CYRILLIC SMALL LETTER ROUNDED VE]
                case '\u2C32': // ⰲ  [GLAGOLITIC SMALL LETTER VEDE]
                    return "v";
                case '\uA760': // Ꝡ  [LATIN CAPITAL LETTER VY]
                    return "VY";
                case '\u24B1': // ⒱  [PARENTHESIZED LATIN SMALL LETTER V]
                    return "(v)";
                case '\uA761': // ꝡ  [LATIN SMALL LETTER VY]
                    return "vy";
                case '\u0174': // Ŵ  [LATIN CAPITAL LETTER W WITH CIRCUMFLEX]
                case '\u01F7': // Ƿ  [LATIN CAPITAL LETTER WYNN]
                case '\u1D21': // ᴡ  [LATIN LETTER SMALL CAPITAL W]
                case '\u1E80': // Ẁ  [LATIN CAPITAL LETTER W WITH GRAVE]
                case '\u1E82': // Ẃ  [LATIN CAPITAL LETTER W WITH ACUTE]
                case '\u1E84': // Ẅ  [LATIN CAPITAL LETTER W WITH DIAERESIS]
                case '\u1E86': // Ẇ  [LATIN CAPITAL LETTER W WITH DOT ABOVE]
                case '\u1E88': // Ẉ  [LATIN CAPITAL LETTER W WITH DOT BELOW]
                case '\u24CC': // Ⓦ  [CIRCLED LATIN CAPITAL LETTER W]
                case '\u2C72': // Ⱳ  [LATIN CAPITAL LETTER W WITH HOOK]
                case '\uFF37': // Ｗ  [FULLWIDTH LATIN CAPITAL LETTER W]
                case '\u16B9': // ᚹ  [RUNIC LETTER WUNJO WYNN W]
                case '\u16A5': // ᚥ  [RUNIC LETTER W]
                    return "W";
                case '\u0175': // ŵ  [LATIN SMALL LETTER W WITH CIRCUMFLEX]
                case '\u01BF': // ƿ  [LATIN LETTER WYNN]
                case '\u028D': // ʍ  [LATIN SMALL LETTER TURNED W]
                case '\u1E81': // ẁ  [LATIN SMALL LETTER W WITH GRAVE]
                case '\u1E83': // ẃ  [LATIN SMALL LETTER W WITH ACUTE]
                case '\u1E85': // ẅ  [LATIN SMALL LETTER W WITH DIAERESIS]
                case '\u1E87': // ẇ  [LATIN SMALL LETTER W WITH DOT ABOVE]
                case '\u1E89': // ẉ  [LATIN SMALL LETTER W WITH DOT BELOW]
                case '\u1E98': // ẘ  [LATIN SMALL LETTER W WITH RING ABOVE]
                case '\u24E6': // ⓦ  [CIRCLED LATIN SMALL LETTER W]
                case '\u2C73': // ⱳ  [LATIN SMALL LETTER W WITH HOOK]
                case '\uFF57': // ｗ  [FULLWIDTH LATIN SMALL LETTER W]
                    return "w";
                case '\u24B2': // ⒲  [PARENTHESIZED LATIN SMALL LETTER W]
                    return "(w)";
                case '\u1E8A': // Ẋ  [LATIN CAPITAL LETTER X WITH DOT ABOVE]
                case '\u1E8C': // Ẍ  [LATIN CAPITAL LETTER X WITH DIAERESIS]
                case '\u24CD': // Ⓧ  [CIRCLED LATIN CAPITAL LETTER X]
                case '\uFF38': // Ｘ  [FULLWIDTH LATIN CAPITAL LETTER X]
                case '\u16EA': // ᛪ  [RUNIC LETTER X]
                    return "X";
                case '\u1D8D': // ᶍ  [LATIN SMALL LETTER X WITH PALATAL HOOK]
                case '\u1E8B': // ẋ  [LATIN SMALL LETTER X WITH DOT ABOVE]
                case '\u1E8D': // ẍ  [LATIN SMALL LETTER X WITH DIAERESIS]
                case '\u2093': // ₓ  [LATIN SUBSCRIPT SMALL LETTER X]
                case '\u24E7': // ⓧ  [CIRCLED LATIN SMALL LETTER X]
                case '\uFF58': // ｘ  [FULLWIDTH LATIN SMALL LETTER X]
                    return "x";
                case '\u24B3': // ⒳  [PARENTHESIZED LATIN SMALL LETTER X]
                    return "(x)";
                case '\u00DD': // Ý  [LATIN CAPITAL LETTER Y WITH ACUTE]
                case '\u0176': // Ŷ  [LATIN CAPITAL LETTER Y WITH CIRCUMFLEX]
                case '\u0178': // Ÿ  [LATIN CAPITAL LETTER Y WITH DIAERESIS]
                case '\u01B3': // Ƴ  [LATIN CAPITAL LETTER Y WITH HOOK]
                case '\u0232': // Ȳ  [LATIN CAPITAL LETTER Y WITH MACRON]
                case '\u024E': // Ɏ  [LATIN CAPITAL LETTER Y WITH STROKE]
                case '\u028F': // ʏ  [LATIN LETTER SMALL CAPITAL Y]
                case '\u1E8E': // Ẏ  [LATIN CAPITAL LETTER Y WITH DOT ABOVE]
                case '\u1EF2': // Ỳ  [LATIN CAPITAL LETTER Y WITH GRAVE]
                case '\u1EF4': // Ỵ  [LATIN CAPITAL LETTER Y WITH DOT BELOW]
                case '\u1EF6': // Ỷ  [LATIN CAPITAL LETTER Y WITH HOOK ABOVE]
                case '\u1EF8': // Ỹ  [LATIN CAPITAL LETTER Y WITH TILDE]
                case '\u1EFE': // Ỿ  [LATIN CAPITAL LETTER Y WITH LOOP]
                case '\u24CE': // Ⓨ  [CIRCLED LATIN CAPITAL LETTER Y]
                case '\uFF39': // Ｙ  [FULLWIDTH LATIN CAPITAL LETTER Y]
                case '\u03A5': // Υ  [GREEK CAPITAL LETTER UPSILON]
                case '\u03D2': // ϒ  [GREEK UPSILON WITH HOOK SYMBOL]
                case '\u038E': // Ύ  [GREEK CAPITAL LETTER UPSILON WITH TONOS]
                case '\u03AB': // Ϋ  [GREEK CAPITAL LETTER UPSILON WITH DIALYTIKA]
                case '\u0419': // Й  [CYRILLIC CAPITAL LETTER SHORT I]
                case '\u16A4': // ᚤ  [RUNIC LETTER Y]
                case '\u16A3': // ᚣ  [RUNIC LETTER YR]
                    return "Y";
                case '\u00FD': // ý  [LATIN SMALL LETTER Y WITH ACUTE]
                case '\u00FF': // ÿ  [LATIN SMALL LETTER Y WITH DIAERESIS]
                case '\u0177': // ŷ  [LATIN SMALL LETTER Y WITH CIRCUMFLEX]
                case '\u01B4': // ƴ  [LATIN SMALL LETTER Y WITH HOOK]
                case '\u0233': // ȳ  [LATIN SMALL LETTER Y WITH MACRON]
                case '\u024F': // ɏ  [LATIN SMALL LETTER Y WITH STROKE]
                case '\u028E': // ʎ  [LATIN SMALL LETTER TURNED Y]
                case '\u1E8F': // ẏ  [LATIN SMALL LETTER Y WITH DOT ABOVE]
                case '\u1E99': // ẙ  [LATIN SMALL LETTER Y WITH RING ABOVE]
                case '\u1EF3': // ỳ  [LATIN SMALL LETTER Y WITH GRAVE]
                case '\u1EF5': // ỵ  [LATIN SMALL LETTER Y WITH DOT BELOW]
                case '\u1EF7': // ỷ  [LATIN SMALL LETTER Y WITH HOOK ABOVE]
                case '\u1EF9': // ỹ  [LATIN SMALL LETTER Y WITH TILDE]
                case '\u1EFF': // ỿ  [LATIN SMALL LETTER Y WITH LOOP]
                case '\u24E8': // ⓨ  [CIRCLED LATIN SMALL LETTER Y]
                case '\uFF59': // ｙ  [FULLWIDTH LATIN SMALL LETTER Y]
                case '\u03C5': // υ  [GREEK SMALL LETTER UPSILON]
                case '\u03CD': // ύ  [GREEK SMALL LETTER UPSILON WITH TONOS]
                case '\u03CB': // ϋ  [GREEK SMALL LETTER UPSILON WITH DIALYTIKA]
                case '\u0439': // й  [CYRILLIC SMALL LETTER SHORT I]
                    return "y";
                case '\u24B4': // ⒴  [PARENTHESIZED LATIN SMALL LETTER Y]
                    return "(y)";
                case '\u0179': // Ź  [LATIN CAPITAL LETTER Z WITH ACUTE]
                case '\u017B': // Ż  [LATIN CAPITAL LETTER Z WITH DOT ABOVE]
                case '\u017D': // Ž  [LATIN CAPITAL LETTER Z WITH CARON]
                case '\u01B5': // Ƶ  [LATIN CAPITAL LETTER Z WITH STROKE]
                case '\u021C': // Ȝ  [LATIN CAPITAL LETTER YOGH]
                case '\u0224': // Ȥ  [LATIN CAPITAL LETTER Z WITH HOOK]
                case '\u1D22': // ᴢ  [LATIN LETTER SMALL CAPITAL Z]
                case '\u1E90': // Ẑ  [LATIN CAPITAL LETTER Z WITH CIRCUMFLEX]
                case '\u1E92': // Ẓ  [LATIN CAPITAL LETTER Z WITH DOT BELOW]
                case '\u1E94': // Ẕ  [LATIN CAPITAL LETTER Z WITH LINE BELOW]
                case '\u24CF': // Ⓩ  [CIRCLED LATIN CAPITAL LETTER Z]
                case '\u2C6B': // Ⱬ  [LATIN CAPITAL LETTER Z WITH DESCENDER]
                case '\uA762': // Ꝣ  [LATIN CAPITAL LETTER VISIGOTHIC Z]
                case '\uFF3A': // Ｚ  [FULLWIDTH LATIN CAPITAL LETTER Z]
                case '\u0396': // Ζ  [GREEK CAPITAL LETTER ZETA]
                case '\u2C8A': // Ⲋ  [COPTIC CAPITAL LETTER SOU]
                case '\u0417': // З  [CYRILLIC CAPITAL LETTER ZE]
                case '\uA640': // Ꙁ  [CYRILLIC CAPITAL LETTER ZEMLYA]
                case '\u2C08': // Ⰸ  [GLAGOLITIC CAPITAL LETTER ZEMLJA]
                case '\u16C9': // ᛉ  [RUNIC LETTER ALGIZ EOLHX]
                case '\u16CE': // ᛎ  [RUNIC LETTER Z]
                    return "Z";
                case '\u017A': // ź  [LATIN SMALL LETTER Z WITH ACUTE]
                case '\u017C': // ż  [LATIN SMALL LETTER Z WITH DOT ABOVE]
                case '\u017E': // ž  [LATIN SMALL LETTER Z WITH CARON]
                case '\u01B6': // ƶ  [LATIN SMALL LETTER Z WITH STROKE]
                case '\u021D': // ȝ  [LATIN SMALL LETTER YOGH]
                case '\u0225': // ȥ  [LATIN SMALL LETTER Z WITH HOOK]
                case '\u0240': // ɀ  [LATIN SMALL LETTER Z WITH SWASH TAIL]
                case '\u0290': // ʐ  [LATIN SMALL LETTER Z WITH RETROFLEX HOOK]
                case '\u0291': // ʑ  [LATIN SMALL LETTER Z WITH CURL]
                case '\u1D76': // ᵶ  [LATIN SMALL LETTER Z WITH MIDDLE TILDE]
                case '\u1D8E': // ᶎ  [LATIN SMALL LETTER Z WITH PALATAL HOOK]
                case '\u1E91': // ẑ  [LATIN SMALL LETTER Z WITH CIRCUMFLEX]
                case '\u1E93': // ẓ  [LATIN SMALL LETTER Z WITH DOT BELOW]
                case '\u1E95': // ẕ  [LATIN SMALL LETTER Z WITH LINE BELOW]
                case '\u24E9': // ⓩ  [CIRCLED LATIN SMALL LETTER Z]
                case '\u2C6C': // ⱬ  [LATIN SMALL LETTER Z WITH DESCENDER]
                case '\uA763': // ꝣ  [LATIN SMALL LETTER VISIGOTHIC Z]
                case '\uFF5A': // ｚ  [FULLWIDTH LATIN SMALL LETTER Z]
                case '\u03B6': // ζ  [GREEK SMALL LETTER ZETA]
                case '\u2C8B': // ⲋ  [COPTIC SMALL LETTER SOU]
                case '\u0437': // з  [CYRILLIC SMALL LETTER ZE]
                case '\uA641': // ꙁ  [CYRILLIC SMALL LETTER ZEMLYA]
                case '\u2C38': // ⰸ  [GLAGOLITIC SMALL LETTER ZEMLJA]
                    return "z";
                case '\u24B5': // ⒵  [PARENTHESIZED LATIN SMALL LETTER Z]
                    return "(z)";
                case '\u2070': // ⁰  [SUPERSCRIPT ZERO]
                case '\u2080': // ₀  [SUBSCRIPT ZERO]
                case '\u24EA': // ⓪  [CIRCLED DIGIT ZERO]
                case '\u24FF': // ⓿  [NEGATIVE CIRCLED DIGIT ZERO]
                case '\uFF10': // ０  [FULLWIDTH DIGIT ZERO]
                    return "0";
                case '\u00B9': // ¹  [SUPERSCRIPT ONE]
                case '\u2081': // ₁  [SUBSCRIPT ONE]
                case '\u2460': // ①  [CIRCLED DIGIT ONE]
                case '\u24F5': // ⓵  [DOUBLE CIRCLED DIGIT ONE]
                case '\u2776': // ❶  [DINGBAT NEGATIVE CIRCLED DIGIT ONE]
                case '\u2780': // ➀  [DINGBAT CIRCLED SANS-SERIF DIGIT ONE]
                case '\u278A': // ➊  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT ONE]
                case '\uFF11': // １  [FULLWIDTH DIGIT ONE]
                    return "1";
                case '\u2488': // ⒈  [DIGIT ONE FULL STOP]
                    return "1.";
                case '\u2474': // ⑴  [PARENTHESIZED DIGIT ONE]
                    return "(1)";
                case '\u00B2': // ²  [SUPERSCRIPT TWO]
                case '\u2082': // ₂  [SUBSCRIPT TWO]
                case '\u2461': // ②  [CIRCLED DIGIT TWO]
                case '\u24F6': // ⓶  [DOUBLE CIRCLED DIGIT TWO]
                case '\u2777': // ❷  [DINGBAT NEGATIVE CIRCLED DIGIT TWO]
                case '\u2781': // ➁  [DINGBAT CIRCLED SANS-SERIF DIGIT TWO]
                case '\u278B': // ➋  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT TWO]
                case '\uFF12': // ２  [FULLWIDTH DIGIT TWO]
                    return "2";
                case '\u2489': // ⒉  [DIGIT TWO FULL STOP]
                    return "2.";
                case '\u2475': // ⑵  [PARENTHESIZED DIGIT TWO]
                    return "(2)";
                case '\u00B3': // ³  [SUPERSCRIPT THREE]
                case '\u2083': // ₃  [SUBSCRIPT THREE]
                case '\u2462': // ③  [CIRCLED DIGIT THREE]
                case '\u24F7': // ⓷  [DOUBLE CIRCLED DIGIT THREE]
                case '\u2778': // ❸  [DINGBAT NEGATIVE CIRCLED DIGIT THREE]
                case '\u2782': // ➂  [DINGBAT CIRCLED SANS-SERIF DIGIT THREE]
                case '\u278C': // ➌  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT THREE]
                case '\uFF13': // ３  [FULLWIDTH DIGIT THREE]
                    return "3";
                case '\u248A': // ⒊  [DIGIT THREE FULL STOP]
                    return "3.";
                case '\u2476': // ⑶  [PARENTHESIZED DIGIT THREE]
                    return "(3)";
                case '\u2074': // ⁴  [SUPERSCRIPT FOUR]
                case '\u2084': // ₄  [SUBSCRIPT FOUR]
                case '\u2463': // ④  [CIRCLED DIGIT FOUR]
                case '\u24F8': // ⓸  [DOUBLE CIRCLED DIGIT FOUR]
                case '\u2779': // ❹  [DINGBAT NEGATIVE CIRCLED DIGIT FOUR]
                case '\u2783': // ➃  [DINGBAT CIRCLED SANS-SERIF DIGIT FOUR]
                case '\u278D': // ➍  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT FOUR]
                case '\uFF14': // ４  [FULLWIDTH DIGIT FOUR]
                    return "4";
                case '\u248B': // ⒋  [DIGIT FOUR FULL STOP]
                    return "4.";
                case '\u2477': // ⑷  [PARENTHESIZED DIGIT FOUR]
                    return "(4)";
                case '\u2075': // ⁵  [SUPERSCRIPT FIVE]
                case '\u2085': // ₅  [SUBSCRIPT FIVE]
                case '\u2464': // ⑤  [CIRCLED DIGIT FIVE]
                case '\u24F9': // ⓹  [DOUBLE CIRCLED DIGIT FIVE]
                case '\u277A': // ❺  [DINGBAT NEGATIVE CIRCLED DIGIT FIVE]
                case '\u2784': // ➄  [DINGBAT CIRCLED SANS-SERIF DIGIT FIVE]
                case '\u278E': // ➎  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT FIVE]
                case '\uFF15': // ５  [FULLWIDTH DIGIT FIVE]
                    return "5";
                case '\u248C': // ⒌  [DIGIT FIVE FULL STOP]
                    return "5.";
                case '\u2478': // ⑸  [PARENTHESIZED DIGIT FIVE]
                    return "(5)";
                case '\u2076': // ⁶  [SUPERSCRIPT SIX]
                case '\u2086': // ₆  [SUBSCRIPT SIX]
                case '\u2465': // ⑥  [CIRCLED DIGIT SIX]
                case '\u24FA': // ⓺  [DOUBLE CIRCLED DIGIT SIX]
                case '\u277B': // ❻  [DINGBAT NEGATIVE CIRCLED DIGIT SIX]
                case '\u2785': // ➅  [DINGBAT CIRCLED SANS-SERIF DIGIT SIX]
                case '\u278F': // ➏  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT SIX]
                case '\uFF16': // ６  [FULLWIDTH DIGIT SIX]
                    return "6";
                case '\u248D': // ⒍  [DIGIT SIX FULL STOP]
                    return "6.";
                case '\u2479': // ⑹  [PARENTHESIZED DIGIT SIX]
                    return "(6)";
                case '\u2077': // ⁷  [SUPERSCRIPT SEVEN]
                case '\u2087': // ₇  [SUBSCRIPT SEVEN]
                case '\u2466': // ⑦  [CIRCLED DIGIT SEVEN]
                case '\u24FB': // ⓻  [DOUBLE CIRCLED DIGIT SEVEN]
                case '\u277C': // ❼  [DINGBAT NEGATIVE CIRCLED DIGIT SEVEN]
                case '\u2786': // ➆  [DINGBAT CIRCLED SANS-SERIF DIGIT SEVEN]
                case '\u2790': // ➐  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT SEVEN]
                case '\uFF17': // ７  [FULLWIDTH DIGIT SEVEN]
                    return "7";
                case '\u248E': // ⒎  [DIGIT SEVEN FULL STOP]
                    return "7.";
                case '\u247A': // ⑺  [PARENTHESIZED DIGIT SEVEN]
                    return "(7)";
                case '\u2078': // ⁸  [SUPERSCRIPT EIGHT]
                case '\u2088': // ₈  [SUBSCRIPT EIGHT]
                case '\u2467': // ⑧  [CIRCLED DIGIT EIGHT]
                case '\u24FC': // ⓼  [DOUBLE CIRCLED DIGIT EIGHT]
                case '\u277D': // ❽  [DINGBAT NEGATIVE CIRCLED DIGIT EIGHT]
                case '\u2787': // ➇  [DINGBAT CIRCLED SANS-SERIF DIGIT EIGHT]
                case '\u2791': // ➑  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT EIGHT]
                case '\uFF18': // ８  [FULLWIDTH DIGIT EIGHT]
                    return "8";
                case '\u248F': // ⒏  [DIGIT EIGHT FULL STOP]
                    return "8.";
                case '\u247B': // ⑻  [PARENTHESIZED DIGIT EIGHT]
                    return "(8)";
                case '\u2079': // ⁹  [SUPERSCRIPT NINE]
                case '\u2089': // ₉  [SUBSCRIPT NINE]
                case '\u2468': // ⑨  [CIRCLED DIGIT NINE]
                case '\u24FD': // ⓽  [DOUBLE CIRCLED DIGIT NINE]
                case '\u277E': // ❾  [DINGBAT NEGATIVE CIRCLED DIGIT NINE]
                case '\u2788': // ➈  [DINGBAT CIRCLED SANS-SERIF DIGIT NINE]
                case '\u2792': // ➒  [DINGBAT NEGATIVE CIRCLED SANS-SERIF DIGIT NINE]
                case '\uFF19': // ９  [FULLWIDTH DIGIT NINE]
                    return "9";
                case '\u2490': // ⒐  [DIGIT NINE FULL STOP]
                    return "9.";
                case '\u247C': // ⑼  [PARENTHESIZED DIGIT NINE]
                    return "(9)";
                case '\u2469': // ⑩  [CIRCLED NUMBER TEN]
                case '\u24FE': // ⓾  [DOUBLE CIRCLED NUMBER TEN]
                case '\u277F': // ❿  [DINGBAT NEGATIVE CIRCLED NUMBER TEN]
                case '\u2789': // ➉  [DINGBAT CIRCLED SANS-SERIF NUMBER TEN]
                case '\u2793': // ➓  [DINGBAT NEGATIVE CIRCLED SANS-SERIF NUMBER TEN]
                    return "10";
                case '\u2491': // ⒑  [NUMBER TEN FULL STOP]
                    return "10.";
                case '\u247D': // ⑽  [PARENTHESIZED NUMBER TEN]
                    return "(10)";
                case '\u246A': // ⑪  [CIRCLED NUMBER ELEVEN]
                case '\u24EB': // ⓫  [NEGATIVE CIRCLED NUMBER ELEVEN]
                    return "11";
                case '\u2492': // ⒒  [NUMBER ELEVEN FULL STOP]
                    return "11.";
                case '\u247E': // ⑾  [PARENTHESIZED NUMBER ELEVEN]
                    return "(11)";
                case '\u246B': // ⑫  [CIRCLED NUMBER TWELVE]
                case '\u24EC': // ⓬  [NEGATIVE CIRCLED NUMBER TWELVE]
                    return "12";
                case '\u2493': // ⒓  [NUMBER TWELVE FULL STOP]
                    return "12.";
                case '\u247F': // ⑿  [PARENTHESIZED NUMBER TWELVE]
                    return "(12)";
                case '\u246C': // ⑬  [CIRCLED NUMBER THIRTEEN]
                case '\u24ED': // ⓭  [NEGATIVE CIRCLED NUMBER THIRTEEN]
                    return "13";
                case '\u2494': // ⒔  [NUMBER THIRTEEN FULL STOP]
                    return "13.";
                case '\u2480': // ⒀  [PARENTHESIZED NUMBER THIRTEEN]
                    return "(13)";
                case '\u246D': // ⑭  [CIRCLED NUMBER FOURTEEN]
                case '\u24EE': // ⓮  [NEGATIVE CIRCLED NUMBER FOURTEEN]
                    return "14";
                case '\u2495': // ⒕  [NUMBER FOURTEEN FULL STOP]
                    return "14.";
                case '\u2481': // ⒁  [PARENTHESIZED NUMBER FOURTEEN]
                    return "(14)";
                case '\u246E': // ⑮  [CIRCLED NUMBER FIFTEEN]
                case '\u24EF': // ⓯  [NEGATIVE CIRCLED NUMBER FIFTEEN]
                    return "15";
                case '\u2496': // ⒖  [NUMBER FIFTEEN FULL STOP]
                    return "15.";
                case '\u2482': // ⒂  [PARENTHESIZED NUMBER FIFTEEN]
                    return "(15)";
                case '\u246F': // ⑯  [CIRCLED NUMBER SIXTEEN]
                case '\u24F0': // ⓰  [NEGATIVE CIRCLED NUMBER SIXTEEN]
                    return "16";
                case '\u2497': // ⒗  [NUMBER SIXTEEN FULL STOP]
                    return "16.";
                case '\u2483': // ⒃  [PARENTHESIZED NUMBER SIXTEEN]
                    return "(16)";
                case '\u2470': // ⑰  [CIRCLED NUMBER SEVENTEEN]
                case '\u24F1': // ⓱  [NEGATIVE CIRCLED NUMBER SEVENTEEN]
                case '\u16EE': // ᛮ  [RUNIC ARLAUG SYMBOL]
                    return "17";
                case '\u2498': // ⒘  [NUMBER SEVENTEEN FULL STOP]
                    return "17.";
                case '\u2484': // ⒄  [PARENTHESIZED NUMBER SEVENTEEN]
                    return "(17)";
                case '\u2471': // ⑱  [CIRCLED NUMBER EIGHTEEN]
                case '\u24F2': // ⓲  [NEGATIVE CIRCLED NUMBER EIGHTEEN]
                case '\u16EF': // ᛯ  [RUNIC TVIMADUR SYMBOL]
                    return "18";
                case '\u2499': // ⒙  [NUMBER EIGHTEEN FULL STOP]
                    return "18.";
                case '\u2485': // ⒅  [PARENTHESIZED NUMBER EIGHTEEN]
                    return "(18)";
                case '\u2472': // ⑲  [CIRCLED NUMBER NINETEEN]
                case '\u24F3': // ⓳  [NEGATIVE CIRCLED NUMBER NINETEEN]
                case '\u16F0': // ᛰ  [RUNIC BELGTHOR SYMBOL]
                    return "19";
                case '\u249A': // ⒚  [NUMBER NINETEEN FULL STOP]
                    return "19.";
                case '\u2486': // ⒆  [PARENTHESIZED NUMBER NINETEEN]
                    return "(19)";
                case '\u2473': // ⑳  [CIRCLED NUMBER TWENTY]
                case '\u24F4': // ⓴  [NEGATIVE CIRCLED NUMBER TWENTY]
                    return "20";
                case '\u249B': // ⒛  [NUMBER TWENTY FULL STOP]
                    return "20.";
                case '\u2487': // ⒇  [PARENTHESIZED NUMBER TWENTY]
                    return "(20)";
                case '\u00AB': // «  [LEFT-POINTING DOUBLE ANGLE QUOTATION MARK]
                case '\u00BB': // »  [RIGHT-POINTING DOUBLE ANGLE QUOTATION MARK]
                case '\u201C': // “  [LEFT DOUBLE QUOTATION MARK]
                case '\u201D': // ”  [RIGHT DOUBLE QUOTATION MARK]
                case '\u201E': // „  [DOUBLE LOW-9 QUOTATION MARK]
                case '\u2033': // ″  [DOUBLE PRIME]
                case '\u2036': // ‶  [REVERSED DOUBLE PRIME]
                case '\u275D': // ❝  [HEAVY DOUBLE TURNED COMMA QUOTATION MARK ORNAMENT]
                case '\u275E': // ❞  [HEAVY DOUBLE COMMA QUOTATION MARK ORNAMENT]
                case '\u276E': // ❮  [HEAVY LEFT-POINTING ANGLE QUOTATION MARK ORNAMENT]
                case '\u276F': // ❯  [HEAVY RIGHT-POINTING ANGLE QUOTATION MARK ORNAMENT]
                case '\uFF02': // ＂  [FULLWIDTH QUOTATION MARK]
                    return "\"";
                case '\u2018': // ‘  [LEFT SINGLE QUOTATION MARK]
                case '\u2019': // ’  [RIGHT SINGLE QUOTATION MARK]
                case '\u201A': // ‚  [SINGLE LOW-9 QUOTATION MARK]
                case '\u201B': // ‛  [SINGLE HIGH-REVERSED-9 QUOTATION MARK]
                case '\u2032': // ′  [PRIME]
                case '\u2035': // ‵  [REVERSED PRIME]
                case '\u2039': // ‹  [SINGLE LEFT-POINTING ANGLE QUOTATION MARK]
                case '\u203A': // ›  [SINGLE RIGHT-POINTING ANGLE QUOTATION MARK]
                case '\u275B': // ❛  [HEAVY SINGLE TURNED COMMA QUOTATION MARK ORNAMENT]
                case '\u275C': // ❜  [HEAVY SINGLE COMMA QUOTATION MARK ORNAMENT]
                case '\uFF07': // ＇  [FULLWIDTH APOSTROPHE]
                    return "\'";
                case '\u2010': // ‐  [HYPHEN]
                case '\u2011': // ‑  [NON-BREAKING HYPHEN]
                case '\u2012': // ‒  [FIGURE DASH]
                case '\u2013': // –  [EN DASH]
                case '\u2014': // —  [EM DASH]
                case '\u207B': // ⁻  [SUPERSCRIPT MINUS]
                case '\u208B': // ₋  [SUBSCRIPT MINUS]
                case '\uFF0D': // －  [FULLWIDTH HYPHEN-MINUS]
                    return "-";
                case '\u2045': // ⁅  [LEFT SQUARE BRACKET WITH QUILL]
                case '\u2772': // ❲  [LIGHT LEFT TORTOISE SHELL BRACKET ORNAMENT]
                case '\uFF3B': // ［  [FULLWIDTH LEFT SQUARE BRACKET]
                    return "[";
                case '\u2046': // ⁆  [RIGHT SQUARE BRACKET WITH QUILL]
                case '\u2773': // ❳  [LIGHT RIGHT TORTOISE SHELL BRACKET ORNAMENT]
                case '\uFF3D': // ］  [FULLWIDTH RIGHT SQUARE BRACKET]
                    return "]";
                case '\u207D': // ⁽  [SUPERSCRIPT LEFT PARENTHESIS]
                case '\u208D': // ₍  [SUBSCRIPT LEFT PARENTHESIS]
                case '\u2768': // ❨  [MEDIUM LEFT PARENTHESIS ORNAMENT]
                case '\u276A': // ❪  [MEDIUM FLATTENED LEFT PARENTHESIS ORNAMENT]
                case '\uFF08': // （  [FULLWIDTH LEFT PARENTHESIS]
                    return "(";
                case '\u2E28': // ⸨  [LEFT DOUBLE PARENTHESIS]
                    return "((";
                case '\u207E': // ⁾  [SUPERSCRIPT RIGHT PARENTHESIS]
                case '\u208E': // ₎  [SUBSCRIPT RIGHT PARENTHESIS]
                case '\u2769': // ❩  [MEDIUM RIGHT PARENTHESIS ORNAMENT]
                case '\u276B': // ❫  [MEDIUM FLATTENED RIGHT PARENTHESIS ORNAMENT]
                case '\uFF09': // ）  [FULLWIDTH RIGHT PARENTHESIS]
                    return ")";
                case '\u2E29': // ⸩  [RIGHT DOUBLE PARENTHESIS]
                    return "))";
                case '\u276C': // ❬  [MEDIUM LEFT-POINTING ANGLE BRACKET ORNAMENT]
                case '\u2770': // ❰  [HEAVY LEFT-POINTING ANGLE BRACKET ORNAMENT]
                case '\uFF1C': // ＜  [FULLWIDTH LESS-THAN SIGN]
                    return "<";
                case '\u276D': // ❭  [MEDIUM RIGHT-POINTING ANGLE BRACKET ORNAMENT]
                case '\u2771': // ❱  [HEAVY RIGHT-POINTING ANGLE BRACKET ORNAMENT]
                case '\uFF1E': // ＞  [FULLWIDTH GREATER-THAN SIGN]
                    return ">";
                case '\u2774': // ❴  [MEDIUM LEFT CURLY BRACKET ORNAMENT]
                case '\uFF5B': // ｛  [FULLWIDTH LEFT CURLY BRACKET]
                    return "{";
                case '\u2775': // ❵  [MEDIUM RIGHT CURLY BRACKET ORNAMENT]
                case '\uFF5D': // ｝  [FULLWIDTH RIGHT CURLY BRACKET]
                    return "}";
                case '\u207A': // ⁺  [SUPERSCRIPT PLUS SIGN]
                case '\u208A': // ₊  [SUBSCRIPT PLUS SIGN]
                case '\uFF0B': // ＋  [FULLWIDTH PLUS SIGN]
                case '\u2020': // †  [DAGGER]
                case '\u16ED': // ᛭  [RUNIC CROSS PUNCTUATION]
                    return "+";
                case '\u207C': // ⁼  [SUPERSCRIPT EQUALS SIGN]
                case '\u208C': // ₌  [SUBSCRIPT EQUALS SIGN]
                case '\uFF1D': // ＝  [FULLWIDTH EQUALS SIGN]
                    return "=";
                case '\uFF01': // ！  [FULLWIDTH EXCLAMATION MARK]
                    return "!";
                case '\u203C': // ‼  [DOUBLE EXCLAMATION MARK]
                    return "!!";
                case '\u2049': // ⁉  [EXCLAMATION QUESTION MARK]
                    return "!?";
                case '\uFF03': // ＃  [FULLWIDTH NUMBER SIGN]
                    return "#";
                case '\uFF04': // ＄  [FULLWIDTH DOLLAR SIGN]
                    return "$";
                case '\u2052': // ⁒  [COMMERCIAL MINUS SIGN]
                case '\uFF05': // ％  [FULLWIDTH PERCENT SIGN]
                    return "%";
                case '\uFF06': // ＆  [FULLWIDTH AMPERSAND]
                    return "&";
                case '\u204E': // ⁎  [LOW ASTERISK]
                case '\uFF0A': // ＊  [FULLWIDTH ASTERISK]
                    return "*";
                case '\uFF0C': // ，  [FULLWIDTH COMMA]
                    return ",";
                case '\uFF0E': // ．  [FULLWIDTH FULL STOP]
                case '\u16EB': // ᛫  [RUNIC SINGLE PUNCTUATION]
                    return ".";
                case '\u2044': // ⁄  [FRACTION SLASH]
                case '\uFF0F': // ／  [FULLWIDTH SOLIDUS]
                    return "/";
                case '\uFF1A': // ：  [FULLWIDTH COLON]
                case '\u16EC': // ᛬  [RUNIC MULTIPLE PUNCTUATION]
                    return ":";
                case '\u204F': // ⁏  [REVERSED SEMICOLON]
                case '\uFF1B': // ；  [FULLWIDTH SEMICOLON]
                    return ";";
                case '\uFF1F': // ？  [FULLWIDTH QUESTION MARK]
                    return "?";
                case '\u2047': // ⁇  [DOUBLE QUESTION MARK]
                    return "??";
                case '\u2048': // ⁈  [QUESTION EXCLAMATION MARK]
                    return "?!";
                case '\uFF20': // ＠  [FULLWIDTH COMMERCIAL AT]
                    return "@";
                case '\uFF3C': // ＼  [FULLWIDTH REVERSE SOLIDUS]
                    return "\\";
                case '\u2038': // ‸  [CARET]
                case '\uFF3E': // ＾  [FULLWIDTH CIRCUMFLEX ACCENT]
                    return "^";
                case '\uFF3F': // ＿  [FULLWIDTH LOW LINE]
                    return "_";
                case '\u2053': // ⁓  [SWUNG DASH]
                case '\uFF5E': // ～  [FULLWIDTH TILDE]
                    return "~";
                case '\u039E': // Ξ  [GREEK CAPITAL LETTER XI]
                case '\u046E': // Ѯ  [CYRILLIC CAPITAL LETTER KSI]
                case '\u2C9C': // Ⲝ  [COPTIC CAPITAL LETTER KSI]
                    return "KS";
                case '\u03BE': // ξ  [GREEK SMALL LETTER XI]
                case '\u046F': // ѯ  [CYRILLIC SMALL LETTER KSI]
                case '\u2C9D': // ⲝ  [COPTIC SMALL LETTER KSI]
                    return "ks";
                case '\u03A8': // Ψ  [GREEK CAPITAL LETTER PSI]
                case '\u1D2A': // ᴪ  [GREEK LETTER SMALL CAPITAL PSI]
                case '\u2CAE': // Ⲯ  [COPTIC CAPITAL LETTER PSI]
                case '\u0470': // Ѱ  [CYRILLIC CAPITAL LETTER PSI]
                    return "PS";
                case '\u03C8': // ψ  [GREEK SMALL LETTER PSI]
                case '\u2CAF': // ⲯ  [COPTIC SMALL LETTER PSI]
                case '\u0471': // ѱ  [CYRILLIC SMALL LETTER PSI]
                    return "ps";
                case '\u0401': // Ё  [CYRILLIC CAPITAL LETTER IO]
                case '\u046C': // Ѭ  [CYRILLIC CAPITAL LETTER IOTIFIED BIG YUS]
                case '\u2C26': // Ⱖ  [GLAGOLITIC CAPITAL LETTER YO]
                case '\u2C29': // Ⱙ  [GLAGOLITIC CAPITAL LETTER IOTATED BIG YUS]
                    return "YO";
                case '\u0451': // ё  [CYRILLIC SMALL LETTER IO]
                case '\u046D': // ѭ  [CYRILLIC SMALL LETTER IOTIFIED BIG YUS]
                case '\u2C56': // ⱖ  [GLAGOLITIC SMALL LETTER YO]
                case '\u2C59': // ⱙ  [GLAGOLITIC SMALL LETTER IOTATED BIG YUS]
                    return "yo";
                case '\u0404': // Є  [CYRILLIC CAPITAL LETTER UKRAINIAN IE]
                case '\u0464': // Ѥ  [CYRILLIC CAPITAL LETTER IOTIFIED E]
                case '\uA656': // Ꙗ  [CYRILLIC CAPITAL LETTER IOTIFIED A]
                case '\u0462': // Ѣ  [CYRILLIC CAPITAL LETTER YAT]
                case '\uA652': // Ꙓ  [CYRILLIC CAPITAL LETTER IOTIFIED YAT]
                case '\u2C21': // Ⱑ  [GLAGOLITIC CAPITAL LETTER YATI]
                    return "YE";
                case '\u0454': // є  [CYRILLIC SMALL LETTER UKRAINIAN IE]
                case '\u0465': // ѥ  [CYRILLIC SMALL LETTER IOTIFIED E]
                case '\uA657': // ꙗ  [CYRILLIC SMALL LETTER IOTIFIED A]
                case '\u0463': // ѣ  [CYRILLIC SMALL LETTER YAT]
                case '\u1C87': // ᲇ  [CYRILLIC SMALL LETTER TALL YAT]
                case '\uA653': // ꙓ  [CYRILLIC SMALL LETTER IOTIFIED YAT]
                case '\u2C51': // ⱑ  [GLAGOLITIC SMALL LETTER YATI]
                    return "ye";
                case '\u0468': // Ѩ  [CYRILLIC CAPITAL LETTER IOTIFIED LITTLE YUS]
                case '\uA65C': // Ꙝ  [CYRILLIC CAPITAL LETTER IOTIFIED CLOSED LITTLE YUS]
                case '\u2C27': // Ⱗ  [GLAGOLITIC CAPITAL LETTER IOTATED SMALL YUS]
                    return "YEN";
                case '\u0469': // ѩ  [CYRILLIC SMALL LETTER IOTIFIED LITTLE YUS]
                case '\uA65D': // ꙝ  [CYRILLIC SMALL LETTER IOTIFIED CLOSED LITTLE YUS]
                case '\u2C57': // ⱗ  [GLAGOLITIC SMALL LETTER IOTATED SMALL YUS]
                    return "yen";
                case '\u0407': // Ї  [CYRILLIC CAPITAL LETTER YI]
                    return "YI";
                case '\u0457': // ї  [CYRILLIC SMALL LETTER YI]
                    return "yi";
                case '\u0416': // Ж  [CYRILLIC CAPITAL LETTER ZHE]
                case '\u2C06': // Ⰶ  [GLAGOLITIC CAPITAL LETTER ZHIVETE]
                    return "ZH";
                case '\u0436': // ж  [CYRILLIC SMALL LETTER ZHE]
                case '\u2C36': // ⰶ  [GLAGOLITIC SMALL LETTER ZHIVETE]
                    return "zh";
                case '\u03A7': // Χ  [GREEK CAPITAL LETTER CHI]
                case '\u2CAC': // Ⲭ  [COPTIC CAPITAL LETTER KHI]
                case '\u0425': // Х  [CYRILLIC CAPITAL LETTER HA]
                case '\u2C18': // Ⱈ  [GLAGOLITIC CAPITAL LETTER HERU]
                case '\u2C22': // Ⱒ  [GLAGOLITIC CAPITAL LETTER SPIDERY HA]
                    return "KH";
                case '\u03C7': // χ  [GREEK SMALL LETTER CHI]
                case '\u1D61': // ᵡ  [MODIFIER LETTER SMALL CHI]
                case '\u1D6A': // ᵪ  [GREEK SUBSCRIPT SMALL LETTER CHI]
                case '\u2CAD': // ⲭ  [COPTIC SMALL LETTER KHI]
                case '\u0445': // х  [CYRILLIC SMALL LETTER HA]
                case '\u2C48': // ⱈ  [GLAGOLITIC SMALL LETTER HERU]
                case '\u2C52': // ⱒ  [GLAGOLITIC SMALL LETTER SPIDERY HA]
                    return "kh";
                case '\u0426': // Ц  [CYRILLIC CAPITAL LETTER TSE]
                case '\u040B': // Ћ  [CYRILLIC CAPITAL LETTER TSHE]
                case '\u2C1C': // Ⱌ  [GLAGOLITIC CAPITAL LETTER TSI]
                    return "TS";
                case '\u0427': // Ч  [CYRILLIC CAPITAL LETTER CHE]
                case '\u2C1D': // Ⱍ  [GLAGOLITIC CAPITAL LETTER CHRIVI]
                case '\u2C2F': // Ⱟ  [GLAGOLITIC CAPITAL LETTER CAUDATE CHRIVI]
                    return "CH";
                case '\u0447': // ч  [CYRILLIC SMALL LETTER CHE]
                case '\u2C4D': // ⱍ  [GLAGOLITIC SMALL LETTER CHRIVI]
                case '\u2C5F': // ⱟ  [GLAGOLITIC SMALL LETTER CAUDATE CHRIVI]
                    return "ch";
                case '\u0428': // Ш  [CYRILLIC CAPITAL LETTER SHA]
                case '\u2C1E': // Ⱎ  [GLAGOLITIC CAPITAL LETTER SHA]
                case '\u16F2': // ᛲ  [RUNIC LETTER SH]
                    return "SH";
                case '\u0448': // ш  [CYRILLIC SMALL LETTER SHA]
                case '\u2C4E': // ⱎ  [GLAGOLITIC SMALL LETTER SHA]
                    return "sh";
                case '\u0429': // Щ  [CYRILLIC CAPITAL LETTER SHCHA]
                    return "SHCH";
                case '\u0449': // щ  [CYRILLIC SMALL LETTER SHCHA]
                    return "shch";
                case '\u042E': // Ю  [CYRILLIC CAPITAL LETTER YU]
                case '\u2C23': // Ⱓ  [GLAGOLITIC CAPITAL LETTER YU]
                    return "YU";
                case '\u044E': // ю  [CYRILLIC SMALL LETTER YU]
                case '\u2C53': // ⱓ  [GLAGOLITIC SMALL LETTER YU]
                    return "yu";
                case '\u042F': // Я  [CYRILLIC CAPITAL LETTER YA]
                    return "YA";
                case '\u044F': // я  [CYRILLIC SMALL LETTER YA]
                    return "ya";
                case '\u2C1B': // Ⱋ  [GLAGOLITIC CAPITAL LETTER SHTA]
                    return "SHT";
                case '\u2C4B': // ⱋ  [GLAGOLITIC SMALL LETTER SHTA]
                    return "sht";
                case '\u042A': // Ъ  [CYRILLIC CAPITAL LETTER HARD SIGN]
                case '\u044A': // ъ  [CYRILLIC SMALL LETTER HARD SIGN]
                case '\u1C86': // ᲆ  [CYRILLIC SMALL LETTER TALL HARD SIGN]
                case '\u042C': // Ь  [CYRILLIC CAPITAL LETTER SOFT SIGN]
                case '\u044C': // ь  [CYRILLIC SMALL LETTER SOFT SIGN]
                case '\u2C20': // Ⱐ  [GLAGOLITIC CAPITAL LETTER YERI]
                case '\u2C50': // ⱐ  [GLAGOLITIC SMALL LETTER YERI]
                case '\u2C2C': // Ⱜ  [GLAGOLITIC CAPITAL LETTER SHTAPIC]
                case '\u2C5C': // ⱜ  [GLAGOLITIC SMALL LETTER SHTAPIC]
                    return string.Empty;
                default:
                    try {
                        char[] charArray = c.ToString().Normalize(NormalizationForm.FormKD).ToCharArray();
                        List<char> charList = new List<char>(1);
                        foreach (char character in charArray) {
                            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                            if (!unicodeCategory.Equals(UnicodeCategory.NonSpacingMark)) {
                                charList.Add(character);
                            }
                        }
                        byte[] bytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(charList.ToArray()));
                        return new string(Encoding.ASCII.GetChars(bytes));
                    } catch (Exception exception) {
                        Debug.WriteLine(exception);
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// Converts the provided string to the corresponding ASCII string
        /// according to the pronunciation in the associated language with
        /// English notation in mind. Symbolic characters are converted based on
        /// their meaning or appearance. Unknown characters and some known
        /// characters are replaced with an empty string and removed. Underscore
        /// belongs to the alphanumeric set.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="encoding">Input encoding.</param>
        /// <param name="conversionOptions">Conversion options.</param>
        /// <returns>Output string.</returns>
        public static string Convert(string str, Encoding encoding, ConversionOptions conversionOptions) {
            char replacingChar = conversionOptions.Equals(ConversionOptions.Alphanumeric) ? '_' : ' ';
            byte[] bytes = Encoding.Convert(encoding, Encoding.Unicode, encoding.GetBytes(str));
            char[] charArray = Encoding.Unicode.GetString(bytes).ToCharArray();
            StringBuilder stringBuilder = new StringBuilder(charArray.Length);
            string buffer = null, recent = null;
            foreach (char c in charArray) {
                recent = Convert(c);
                if (buffer != null && buffer.Length > 1 && recent.Length > 0 && recent[0] > '\u0060' && recent[0] < '\u007B') {
                    buffer = buffer.Substring(0, 1) + buffer.Substring(1).ToLower();
                }
                stringBuilder.Append(buffer);
                buffer = recent;
            }
            if (buffer != null && buffer.Length > 1 && recent.Length > 0 && recent[0] > '\u0060' && recent[0] < '\u007B') {
                buffer = buffer.Substring(0, 1) + buffer.Substring(1).ToLower();
            }
            stringBuilder.Append(buffer);
            List<char> charList = new List<char>(stringBuilder.Length);
            foreach (char c in stringBuilder.ToString().ToCharArray()) {
                switch (conversionOptions) {
                    case ConversionOptions.SafePath:
                        if (Path.GetInvalidPathChars().Contains(c)) {
                            if (charList.Count > 0 && charList[charList.Count - 1] != replacingChar) {
                                charList.Add(replacingChar);
                            }
                        } else {
                            charList.Add(c);
                        }
                        break;
                    case ConversionOptions.SafeFileName:
                        if (Path.GetInvalidFileNameChars().Contains(c)) {
                            if (charList.Count > 0 && charList[charList.Count - 1] != replacingChar) {
                                charList.Add(replacingChar);
                            }
                        } else {
                            charList.Add(c);
                        }
                        break;
                    case ConversionOptions.Alphanumeric:
                        if (c > '\u002F' && c < '\u003A' || c > '\u0040' && c < '\u005B' || c > '\u0060' && c < '\u007B') {
                            charList.Add(c);
                        } else if (charList.Count > 0 && charList[charList.Count - 1] != replacingChar) {
                            charList.Add(replacingChar);
                        }
                        break;
                    default:
                        charList.Add(c);
                        break;
                }
            }
            if (charList.Count > 0 && charList[charList.Count - 1] == replacingChar) {
                charList.RemoveAt(charList.Count - 1);
            }
            if (charList.Count > 0 && charList[0] == replacingChar) {
                charList.RemoveAt(0);
            }
            return new string(charList.ToArray());
        }

        /// <summary>
        /// Converts the provided string to the corresponding ASCII string
        /// according to the pronunciation in the associated language with
        /// English notation in mind. Symbolic characters are converted based on
        /// their meaning or appearance. Unknown characters and some known
        /// characters are replaced with an empty string and removed. The output
        /// string can contain all printable ASCII characters. 
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="encoding">Input encoding.</param>
        /// <returns>Output string.</returns>
        public static string Convert(string str, Encoding encoding) => Convert(str, encoding, ConversionOptions.Full);

        /// <summary>
        /// Conversion options.
        /// </summary>
        public enum ConversionOptions {
            ///<summary>
            ///Conversion to all possible printable ASCII characters.
            ///</summary>
            Full,
            ///<summary>
            ///Conversion to ASCII without invalid path characters.
            ///</summary>
            SafePath,
            ///<summary>
            ///Conversion to ASCII without invalid file name characters.
            ///</summary>
            SafeFileName,
            ///<summary>
            ///Conversion to alphanumeric-only ASCII characters (including
            ///underscore).
            ///</summary>
            Alphanumeric
        }
    }
}
