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

namespace Ntx.GD
{
	public class main
	{
		public static void Main()
		{
			GD image = new GD( 256 + 384, 384, true );
			
			bool saveAlpha = image.SaveAlpha;

			GDColor white = image.ColorAllocate( 255, 255, 255 );			
			
			image.FilledRectangle( 0, 0, image.Width, image.Height, white );
			
			/* Set transparent color. */
			image.ColorTransparent( white );
			
			GD import = new GD( GD.FileType.Png, "../Img/demoin.png" );
			
			/* Now copy, and magnify as we do so */
			image.CopyResampled( import, 32, 32, 0, 0, 192, 192, 255, 255 );
			
			/* Now display variously rotated space shuttles in a circle of our own */
			for( int a = 0; ( a < 360 ); a += 45 )
	        {
 		    	int cx = (int) Math.Floor( Math.Cos ( a * .0174532925 ) * 127 );
   		       	int cy = (int) Math.Floor( -Math.Sin ( a * .0174532925 ) * 127 );

     		   	image.CopyRotated( import, 
					256 + 192 + cx, 192 + cy,
					0, 0, import.Width, import.Height, a );
        	}
			
			GDColor red = image.ColorAllocate( 255, 0, 0 );
  			GDColor green = image.ColorAllocate( 0, 255, 0 );
  			GDColor blue = image.ColorAllocate( 0, 0, 255 );
  
  			/* Fat Rectangle */
  			image.SetThickness( 4 );
  			image.Line( 16, 16, 240, 16, green );
  			image.Line( 240, 16, 240, 240, green );
  			image.Line( 240, 240, 16, 240, green );
  			image.Line( 16, 240, 16, 16, green );
  			image.SetThickness( 1 );
			
  			/* Circle */
  			image.Arc( 128, 128, 60, 20, 0, 720, blue );
  			
  			/* Arc */
  			image.Arc( 128, 128, 40, 40, 90, 270, blue );
  			/* Flood fill: doesn't do much on a continuously
		     	variable tone jpeg original. */
			image.Fill( 8, 8, blue );

			/* Polygon */
			ArrayList list = new ArrayList();
			list.Add( new Point( 64, 0 ) );
			list.Add( new Point( 0, 128 ) );
			list.Add( new Point( 128, 128 ) );
			image.FilledPolygon( list, green );						
			
			 /* 2.0.12: Antialiased Polygon */
  			image.SetAntiAliased( green );
  			for( int i = 0; ( i < 3 ); i++ ) 
   			{
				((Point) list[i]).X += 128;
	    	}
			image.FilledPolygon( list, GD.GD_ANTIALIASED );


			/* Brush. A fairly wild example also involving a line style! */
	      	int[] style = new int[8];
      		GD brush = new GD( 16, 16, true );
      		brush.CopyResized( import,
				0, 0, 0, 0,
				brush.Width, brush.Height,
                import.Width, import.Height );
	      	image.SetBrush( brush );
	    
	      	/* With a style, so they won't overprint each other.
	        Normally, they would, yielding a fat-brush effect. */
	      	style[0] = 0;
	      	style[1] = 0;
	      	style[2] = 0;
	      	style[3] = 0;
	      	style[4] = 0;
	      	style[5] = 0;
	      	style[6] = 0;
	      	style[7] = 1;
	      	image.SetStyle( style );
	      	
	      	/* Draw the styled, brushed line */
	      	image.Line( 0, 255, 255, 0, GD.GD_STYLED_BRUSHED );

  			/* Text (non-truetype; see gdtestft for a freetype demo) */
  			ArrayList fonts = new ArrayList();
			fonts.Add( new Font( Font.Type.Tiny ) );
  			fonts.Add( new Font( Font.Type.Small ) );
  			fonts.Add( new Font( Font.Type.MediumBold ) );
  			fonts.Add( new Font( Font.Type.Large ) );
  			fonts.Add( new Font( Font.Type.Giant ) );
  			
  			int y = 0;
  			for( int i = 0; (i <= 4); i++ ) 
  			{
    				image.String( ((Font) fonts[i]), 32, 32 + y, "hi", red );
    				y += ((Font) fonts[i]).Height();
  			}
  			
  			y = 0;
  			for( int i = 0; (i <= 4); i++ ) 
  			{
	    		image.StringUp( ((Font) fonts[i]), 64 + y, 64, "hi", red );
    			y += ((Font) fonts[i]).Height();
  			}


 			/* Random antialiased lines; coordinates all over the image,
    			but the output will respect a small clipping rectangle */
  			image.SetClip( 0, image.Height - 100, 100, image.Height );
  			
  			/* Fixed seed for reproducibility of results */
  			Random ran = new Random( 100 );
			
			for( int i = 0; (i < 100); i++ ) 
			{
			    int x1 = ran.Next() % image.Width;
			    int y1 = ran.Next() % image.Height;
			    int x2 = ran.Next() % image.Width;
			    int y2 = ran.Next() % image.Height;
			
			    image.SetAntiAliased( white );
			    image.Line( x1, y1, x2, y2, GD.GD_ANTIALIASED );
			}

			/* Make output image interlaced (progressive, in the case of JPEG) */
			image.Interlace = true;

			image.Save( GD.FileType.Png, "demoout.png", 1 );

  			/* 2.0.12: also write a paletteized version */
			image.TrueColorToPalette( 0, 256 );

			image.Save( GD.FileType.Png, "demooutp.png", 1 );

			using(FileStream fs = File.OpenWrite(@"stream.jpg"))
			{
				image.Save(Ntx.GD.GD.FileType.Jpeg, (System.IO.Stream)fs,100);
				fs.Close();
			}

			using(FileStream fs = File.OpenWrite(@"stream.png"))
			{
				image.Save(Ntx.GD.GD.FileType.Png, (System.IO.Stream)fs,100);
				fs.Close();
			}

			using(FileStream fs = File.OpenWrite(@"stream1.jpg"))
			{
				image.Save((System.IO.Stream)fs);
				fs.Close();
			}

			image.Save("determinetype.jpg");

			/* Free resources */
			brush.Dispose();
			import.Dispose();
			image.Dispose();	
		
			TestGif();
			MakeAnimGif();
		}

		static void TestGif()
		{
			GD img = new GD(GD.FileType.Gd2, "../Img/demoin.gd2");
			img.Save(GD.FileType.Gif, "demoout.gif");
		}

		static void MakeAnimGif()
		{
			using (FileStream fs = File.OpenWrite(@"anim.gif"))
			{
				GD host = CreateGifFrame(0);
				host.GifAnimBegin(fs);
				GD prev = null;
				for (int i=0; i<20; i++)
				{
					GD frame = CreateGifFrame(i);
					frame.GifAnimAdd(fs, 0, 0,0,2, GD.Disposal.None, prev);
					//frame.Save("frame" + i + ".gif");
					//frame.Save(GD.FileType.Png, "frame" + i + ".png", 1);
					prev = frame;
				}
				host.GifAnimEnd(fs);
				fs.Close();
			}

		}

		static GD CreateGifFrame(int frame)
		{
			GD img = new GD(256 + 384, 384, false);
			GDColor black = img.ColorAllocate(0, 0, 0);
			GDColor white = img.ColorAllocate(255, 255, 255);
			GDColor red = img.ColorAllocate(255, 0, 0);
			img.Rectangle (frame * 5, frame * 5, 50 + frame * 5 , 50+frame*5, red);
			return img;
		}
	}
}
