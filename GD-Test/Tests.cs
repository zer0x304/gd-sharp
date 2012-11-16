/*************************************************************************************************************************\

Author:		Mircea-Cristian Racasan <darx_kies@gmx.net>
Copyright: 	2005 by Mircea-Cristian Racasan

This program is free software; you can redistribute it and/or modify it under the terms of the 
GNU General Public License as published by the Free Software Foundation; either version 2 of the License, 
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program; 
if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

\*************************************************************************************************************************/

using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Ntx.GD;

[TestFixture]
public class Tests
{
	GD gd;
	
	[TestFixtureSetUp]
	public void SetUp()
	{
		gd = new GD( 100, 100, true );
	}

	[TestFixtureTearDown] 
	public void TearDown()
	{
	   	gd.Dispose();
	}

	[Test]
	public void DefaultSaveAlphaFlag()
	{
		Assert.AreEqual( false, gd.SaveAlpha, "saveAlphaFlag");
	}

	[Test]
	public void DefaultAlphaBlending()
	{
		Assert.AreEqual( true, gd.AlphaBlending, "alphaBlendingFlag");
	}

	[Test]
	public void Size()
	{
		Assert.AreEqual( 100, gd.Width, "Width" );
		Assert.AreEqual( 100, gd.Height, "Height" );
	}
	
	[Test]
	public void Pixel()
	{
		gd.SetPixel( 10, 10, (GDColor)123 );
		
		GDColor color = gd.GetPixel( 10, 10 );
		
		Assert.AreEqual( (GDColor) 123, color, "Color" );
	}
	
	[Test]
	public void Transparent()
	{
		GDColor white = gd.ColorAllocate( 255, 255, 255 );
		GDColor black = gd.ColorAllocate( 0, 0, 0 );
		GDColor green = gd.ColorAllocate( 2, 22, 222 );
		gd.ColorTransparent( green );
		GDColor color = gd.Transparent;
		
		Assert.AreEqual( green, color, "Transparent" );
	}
	
	[Test]
	public void Interlace()
	{
		gd.Interlace = true ;
		bool value = gd.Interlace;
		
		Assert.AreEqual( true, value, "Transparent" );
	}
	
	[Test]
	public void TrueColor()
	{
		int value = gd.GetTrueColor();

		Assert.AreEqual( 1, value, "Truecolor" );
	}
	
	[Test]
	public void Clip()
	{
		int x1, x2, y1, y2;
		x1 = x2 = y1 = y2 = 0;
		gd.SetClip( 5, 5, 10, 10 );
		gd.GetClip( ref x1, ref y1, ref x2, ref y2 );
		
		Assert.AreEqual( 5, x1, "x1" );
		Assert.AreEqual( 5, y1, "y1" );
		Assert.AreEqual( 10, x2, "x2" );
		Assert.AreEqual( 10, y2, "y2" );
	}
	
	[Test]
	public void Save()
	{
		GDColor white = gd.ColorAllocate( 255, 255, 255 );
		GDColor blue = gd.ColorAllocate( 0, 0, 255 );
		GDColor black = gd.ColorAllocate( 0, 0, 0 );
		Font small = new Font( Font.Type.Small );
		
		gd.FilledRectangle( 0, 0, 99, 99, white );
		gd.Line( 0, 0, 99, 99, blue );
		
		ArrayList list = new ArrayList();
		list.Add( new Point( 50, 0 ) );
		list.Add( new Point( 99, 99 ) );
		list.Add( new Point( 0, 99 ) );
		
		gd.Polygon( list, blue );

		gd.String( small, 0, 0, "darxkies", black );
		gd.Char( small, 0, 50, 'X', black );
		
		//Console.WriteLine( gd.ImageStringFT( list, black, "/usr/X11R6/lib/X11/fonts/truetype/arial.ttf", 8, 0, 0, 50, "ginger", true ) );
		
		Assert.AreEqual( true, gd.Save( GD.FileType.Jpeg, "test.jpeg", 30 ), "Save" );

        using(FileStream fs = File.OpenWrite(@"stream-test.jpg"))
        {
			Assert.AreEqual( true, gd.Save((System.IO.Stream) fs), "Save-Stream");
            fs.Close();
        }
	}

	[Test]
	public void Load()
	{
		if (!File.Exists("test.jpeg"))
		{
			Console.WriteLine("Save test must be run first for this to be meaningful");
			return;
		}

		gd.Dispose();
			
		gd = new GD( GD.FileType.Jpeg, "test.jpeg" );				
	}
}
