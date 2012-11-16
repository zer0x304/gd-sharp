/*************************************************************************************************************************\

Author:		Mircea-Cristian Racasan <darx_kies@gmx.net>
            Chris Turchin <chris@turchin.net>
			Kevin Tam <kevin@glorat.net>
Copyright: 	2005 by Mircea-Cristian Racasan
            Portions by Kevin Tam 2005

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
	public class GD: IDisposable
	{
		#region GD Constants
	   	/***********************************************************************************\
	   	 GD Constants
	   	\***********************************************************************************/
		public const int 	GD_MAX_COLORS = 256;
		public static GDColor GD_ANTIALIASED = new GDColor(-7);
		public const int 	GD_BRUSHED = -3;
		public const int	GD_TILED = -5;
		public static GDColor GD_STYLED_BRUSHED = new GDColor(-4);
		public const int	GD_STYLED = -2;
		public const int	GD_DASH_SIZE = 4;
		public const int	GD_TRANSPARENT = -7;
		// for saving GD2 format
		private const int	GD2_FMT_COMPRESSED = 2;
                    
		public enum ArcType: int
		{
			Arc = 0,
			Pie = 0,
			Chrod = 1,
			NoFill = 2,
			Edged = 4
		}

		public enum FileType: int
		{
			Jpeg = 0,
			Png,
			Gd, 
			Gd2,
			WBMP,
			Xbm,
			Xpm,
			Gif
		}

		public enum Disposal : int
		{
			Unknown = 0,
			None,
			RestoreBackground,
			RestorePrevious
		};

		
		[StructLayout( LayoutKind.Sequential )]
		private struct GDImage
		{
			public IntPtr 	pixels;
			public int 		sx;
			public int 		sy;
			public int 		colorsTotal;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	red;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	green;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	blue;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	open;
			public int 		transparent;
			public IntPtr 	polyInts;
			public int 		polyAllocated;
			public IntPtr 	brush;
			public IntPtr 	tile;  
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	brushColorMap;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	tileColorMap;
			public int 		styleLength;
			public int 		stylePos;
			public IntPtr 	style;
			public int 		interlace;
			public int 		thick;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=GD_MAX_COLORS )]
			public int[] 	alpha; 
			public int 		trueColor;
			public IntPtr	tpixels;
			public int		alphaBlendingFlag;
			public int		saveAlphaFlag;



			private static Hashtable mFieldOffsets;
			static GDImage()
			{
				// Cache field offsets for faster lookup
				mFieldOffsets = new Hashtable();
				Type t = typeof(GDImage);
				foreach (System.Reflection.FieldInfo field in t.GetFields())
				{
					mFieldOffsets[field.Name] = Marshal.OffsetOf(t, field.Name).ToInt32();
				}
			}

			/// <summary>
			/// Returns the offset in bytes from the start of the GDImage structure
			/// to the given field
			/// </summary>
			/// <param name="field">Field name</param>
			/// <returns>Offset in bytes</returns>
			public static int GetOffset(string field)
			{
				return (int)mFieldOffsets[field];
			}
		}

		#endregion

		#region Class member variables
		private HandleRef 	handle;
		private bool 		disposed = false;
		#endregion

		#region Color Methods
	   	/***********************************************************************************\
	   	 GD Color
	   	\***********************************************************************************/

	   	public GDColor ColorAllocate( int r, int g, int b )
	   	{
	   		return (GDColor) GDImport.gdImageColorAllocate( this.Handle, r, g, b );
	   	}

	   	public GDColor ColorAllocateAlpha( int r, int g, int b, int a )
	   	{
	   		return (GDColor) GDImport.gdImageColorAllocateAlpha( this.Handle, r, g, b, a );
	   	}

   		public void ColorDeallocate( GDColor color )
	   	{
	   		GDImport.gdImageColorDeallocate( this.Handle, color );
	   	}

		public GDColor ColorClosest( int r, int g, int b )
	   	{
	   		return (GDColor) GDImport.gdImageColorClosest( this.Handle, r, g, b );
	   	}

	   	public GDColor ColorClosestAlpha( int r, int g, int b, int a )
	   	{
	   		return (GDColor) GDImport.gdImageColorClosestAlpha( this.Handle, r, g, b, a );
	   	}

		public GDColor ColorClosestHWB( int r, int g, int b )
	   	{
	   		return (GDColor) GDImport.gdImageColorClosestHWB( this.Handle, r, g, b );
	   	}

	   	public GDColor ColorExact( int r, int g, int b )
	   	{
	   		return (GDColor) GDImport.gdImageColorExact( this.Handle, r, g, b );
	   	}

		public GDColor ColorResolve( int r, int g, int b )
	   	{
	   		return (GDColor) GDImport.gdImageColorResolve( this.Handle, r, g, b );
	   	}

		public GDColor ColorResolveAlpha( int r, int g, int b, int a )
	   	{
	   		return (GDColor) GDImport.gdImageColorResolveAlpha( this.Handle, r, g, b, a );
	   	}
	   	
		public void ColorTransparent( GDColor color )
	   	{
	   		GDImport.gdImageColorTransparent( this.Handle, color );
	   	}
		#endregion

		#region Drawing/Styling/Brushing/Tiling/Filling
	   	/***********************************************************************************\
	   	 GD Drawing/Styling/Brushing/Tiling/Filling
	   	\***********************************************************************************/

	   	public void SetPixel( int x, int y, GDColor color )
	   	{
	   		GDImport.gdImageSetPixel( this.Handle, x, y, color );
	   	}

	   	public void Line( int x1, int y1, int x2, int y2, GDColor color )
	   	{
	   		GDImport.gdImageLine( this.Handle, x1, y1, x2, y2, color );
	   	}

 	  	public void DashedLine( int x1, int y1, int x2, int y2, GDColor color )
	   	{
	   		GDImport.gdImageDashedLine( this.Handle, x1, y1, x2, y2, color );
	   	}
	   	
	
	   	public void Rectangle( int x1, int y1, int x2, int y2, GDColor color)
	   	{
	   		GDImport.gdImageRectangle( this.Handle, x1, y1, x2, y2, color);
	   	}

	   	public void FilledRectangle( int x1, int y1, int x2, int y2, GDColor color )
	   	{
	   		GDImport.gdImageFilledRectangle( this.Handle, x1, y1, x2, y2, color );
	   	}
	   	

 		public void Polygon( ArrayList list, GDColor color )
	   	{
			int[] intList = Point.GetIntArray( list );
	   		
			GDImport.gdImagePolygon( this.Handle, intList, list.Count, color );
	   	}
	   	
	
		public void FilledPolygon( ArrayList list, GDColor color )
	   	{
			int[] intList = Point.GetIntArray( list );
			
	   		GDImport.gdImageFilledPolygon( this.Handle, intList, list.Count, color );
	   	}
	   	
	
		public void Arc( int cx, int cy, int w, int h, int s, int e, GDColor color )
	   	{
	   		GDImport.gdImageArc( this.Handle, cx, cy, w, h, s, e, color );
	   	}


	   	public void FilledArc( int cx, int cy, int w, int h, int s, int e, GDColor color, int style )
	   	{
	   		GDImport.gdImageFilledArc( this.Handle, cx, cy, w, h, s, e, color, style );
	   	}
	   	
	
		public void FilledEllipse( int cx, int cy, int w, int h, GDColor color )
	   	{
	   		GDImport.gdImageFilledEllipse( this.Handle, cx, cy, w, h, color );
	   	}
	   	
	 	public void FillToBorder( int x, int y, int border, GDColor color )
	   	{
	   		GDImport.gdImageFillToBorder( this.Handle, x, y, border, color );
	   	}
	   	
		public void Fill( int x, int y, GDColor color )
	   	{
	   		GDImport.gdImageFill( this.Handle, x, y, color );
	   	}
	
		public void SetAntiAliased( GDColor c )
	   	{
	   		GDImport.gdImageSetAntiAliased( this.Handle, c);
	   	}
	   		
	   	public void SetAntiAliasedDontBlend( GDColor c )
	   	{
	   		GDImport.gdImageSetAntiAliasedDontBlend( this.Handle, c );
	   	}
	   	
		public void SetBrush( GD brush )
		{
			GDImport.gdImageSetBrush( this.Handle, brush.GetHandle() );	
		}
		
	   	
	 	public void SetTile( GD brush )
		{
			GDImport.gdImageSetTile( this.Handle, brush.GetHandle() );	
		}

	  	public void SetStyle( int[] style )
		{
			GDImport.gdImageSetStyle( this.Handle, style, style.Length );
		}
		
	   	public void SetThickness( int thickness )
		{
			GDImport.gdImageSetThickness( this.Handle, thickness );
		}
		
  		public bool AlphaBlending
		{
			get
			{
				return IntToBool( GetStructInt("alphaBlendingFlag"));
			}
			set 
			{
				GDImport.gdImageAlphaBlending( this.Handle, BoolToInt(value) );
			}
		}
		 	
 		public bool SaveAlpha
		{
			get
			{
				return IntToBool( GetStructInt("saveAlphaFlag") );
			}
			set
			{
				GDImport.gdImageSaveAlpha( this.Handle, BoolToInt(value) );
			}
		}
	   	
		public void SetClip( int x1, int y1, int x2, int y2 )
		{
			GDImport.gdImageSetClip( this.Handle, x1, y1, x2, y2 );
		}
		#endregion
	   	
		#region Font/Text
		/***********************************************************************************\
	   	 GD Font/Text
	   	\***********************************************************************************/

		public void Char( Font font, int x, int y, int c, GDColor color )
		{
			HandleRef fontHandle =  font.GetHandle();
			
	   		GDImport.gdImageChar( this.Handle, fontHandle, x, y, c, color);
		}
		
		public void CharUp( Font font, int x, int y, int c, GDColor color )
		{
			HandleRef fontHandle =  font.GetHandle();
			
	   		GDImport.gdImageCharUp( this.Handle, fontHandle, x, y, c, color);
		}

		public void String( Font font, int x, int y, string message, GDColor color )
		{
			HandleRef fontHandle =  font.GetHandle();
			
	   		GDImport.gdImageString( this.Handle, fontHandle, x, y, message, color);
		}
		

		public void StringUp( Font font, int x, int y, string message, GDColor color )
		{
			HandleRef fontHandle =  font.GetHandle();
			
	   		GDImport.gdImageStringUp( this.Handle, fontHandle, x, y, message, color);
		}
		

		public string StringFT( ArrayList list, int fg, string fontname, double ptsize, double angle, int x, int y, string message, bool draw )
		{
			int[] brect = new int[8];
			
	   		string result = GDImport.gdImageStringFT( draw? this.Handle: IntPtr.Zero, brect, fg, fontname, ptsize, angle, x, y, message );
	   		
	   		list.Clear();
	   		list.Add( new Point( brect[0], brect[1] ) );
	   		list.Add( new Point( brect[2], brect[3] ) );
	   		list.Add( new Point( brect[4], brect[5] ) );
	   		list.Add( new Point( brect[6], brect[7] ) );
	   			   		
	   		return result;
		}
		

		public string StringFTCircle( int cx, int cy, double radius, double textRadius, double fillPortion, string font, double points, string top, string bottom, GDColor fgcolor )
		{
			return GDImport.gdImageStringFTCircle( this.Handle, cx, cy, radius, textRadius, fillPortion, font, points, top, bottom, fgcolor );
		}
		#endregion
	
		#region GD Creation/Destruction/Loading/Saving
	   	/***********************************************************************************\
	   	 GD Creation/Destruction/Loading/Saving
	   	\***********************************************************************************/

	   	public GD( int width, int height, bool trueColor )
	   	{
	   		IntPtr imageHandle;
	   		
	   		if( trueColor )
	   			imageHandle = GDImport.gdImageCreateTrueColor( width, height);
	   		else
	   			imageHandle = GDImport.gdImageCreate( width, height);
	   		
         	if( imageHandle == IntPtr.Zero )
	   			throw new ApplicationException( "ImageCreate failed." );
	   			
			this.handle = new HandleRef( this, imageHandle );
			
			//this.imageData = (GDImage) Marshal.PtrToStructure( this.Handle, typeof( GDImage ) );
	   	}

		public GD( FileType type, string filename )
		{
	   		IntPtr imageHandle;
			IntPtr fileHandle = GDImport.fopen( filename, "rb" );
	   		
	   		if( fileHandle == IntPtr.Zero )
	   			throw new ApplicationException( filename + " not found." );
	   			
			switch( type )
			{
				case FileType.Jpeg:
					imageHandle = GDImport.gdImageCreateFromJpeg( fileHandle );
					break;
				case FileType.Png:
					imageHandle = GDImport.gdImageCreateFromPng( fileHandle );
					break;
				case FileType.Gd:
					imageHandle = GDImport.gdImageCreateFromGd( fileHandle );
					break; 
				case FileType.Gd2:
					imageHandle = GDImport.gdImageCreateFromGd2( fileHandle );
					break;
				case FileType.WBMP:
					imageHandle = GDImport.gdImageCreateFromWBMP( fileHandle );
					break;
				case FileType.Xbm:
					imageHandle = GDImport.gdImageCreateFromXbm( fileHandle );
					break;
				case FileType.Xpm:
					imageHandle = GDImport.gdImageCreateFromXpm( fileHandle );
					break;
 				case FileType.Gif:
 					imageHandle = GDImport.gdImageCreateFromGif( fileHandle );
 					break;
				default:
			   		GDImport.fclose( fileHandle );
					throw new ApplicationException( type + " is unknown import type." );
			}

	   		GDImport.fclose( fileHandle );
			
			if( imageHandle == IntPtr.Zero )
	   			throw new ApplicationException( "ImageCreateFrom failed." );
	   			
			this.handle = new HandleRef( this, imageHandle );
			
			//this.imageData = (GDImage) Marshal.PtrToStructure( this.Handle, typeof( GDImage ) );
		}

		// Parameter:
		// PNG - level of compression ( 0 - no compression ... )
		// Jpeg - quality
	   	public bool Save( FileType type, string filename, int parameter )
	   	{
	   		IntPtr fileHandle = GDImport.fopen( filename, "wb" );
	   		
	   		if( fileHandle == IntPtr.Zero )
	   			return false;
	   		
			switch( type )
			{
				case FileType.Jpeg:
					GDImport.gdImageJpeg( this.Handle, fileHandle, parameter );
					break;
				case FileType.Png:
					GDImport.gdImagePngEx( this.Handle, fileHandle, parameter );
					break;
				case FileType.Gd:
					GDImport.gdImageGd( this.Handle, fileHandle );
					break; 
				case FileType.Gd2:
					GDImport.gdImageGd2( this.Handle, fileHandle );
					break;
				case FileType.WBMP:
					GDImport.gdImageWBMP( this.Handle, parameter, fileHandle );
					break;
				case FileType.Gif:
					GDImport.gdImageGif (this.Handle, fileHandle);
					break;
				case FileType.Xbm:
					//gdImageXbm( this.Handle, fileHandle );
					//break;
				case FileType.Xpm:
					//gdImageXpm( this.Handle, fileHandle );
					//break;
					throw new ApplicationException( type + " not implemented." );
				default:
			   		GDImport.fclose( fileHandle );
					throw new ApplicationException( type + " is an unknown file type." );
			}

	   		GDImport.fclose( fileHandle );
	   		
	   		return true;
	   	} 
	   	

		public bool Save(string filename)
		{
			return this.Save(filename,75);
		}
		
		public bool Save(string filename, int quality)
		{
			FileInfo fi;
			FileType ft;
			string ext = "";
			
			fi = new FileInfo(filename);
			ext = fi.Extension.ToLower();

			switch(ext)
			{
				case ".jpg":
				case ".jpeg":
				case ".jpe":
					ft = FileType.Jpeg;
					break;
				case ".png":
					ft = FileType.Png;
					break;
				case ".wbmp":
					ft = FileType.WBMP;
					break;
				case ".gd":
					ft = FileType.Gd;
					break;
				case ".gd2":
					ft = FileType.Gd2;
					break;
				case ".gif":
					ft = FileType.Gif;
					break;
				default:
					throw new ApplicationException("cannot determine file type. pass FileType parameter." );
			}

			return this.Save(ft,filename,quality);
		}

		public bool Save(FileType type, string filename)
		{
			return this.Save(type,filename,75);
		}

		public bool Save(Stream outstream)
		{
			//defaulting to jpg, this could be PNG i suppose, but i use jpg more often
			return this.Save(FileType.Jpeg,outstream,75);
		}
		
		public bool Save(FileType type, Stream outstream)
		{
			return this.Save(type,outstream,75);
		}

		public bool Save(FileType type, Stream outstream, int parameter )
	   	{
			try
			{
				IntPtr 	imgDataHandle = IntPtr.Zero;
				int len;
				try
				{
					switch( type )
					{
						case FileType.Jpeg:
							imgDataHandle = GDImport.gdImageJpegPtr( this.Handle, out len, parameter );
							break;
						case FileType.Png:
							imgDataHandle = GDImport.gdImagePngPtr( this.Handle, out len);
							break;
						case FileType.Gd:
							imgDataHandle = GDImport.gdImageGdPtr( this.Handle, out len);
							break; 
						case FileType.Gd2:
							imgDataHandle = GDImport.gdImageGd2Ptr( this.Handle, 0, GD2_FMT_COMPRESSED, out len);
							break; 
						case FileType.WBMP:
							imgDataHandle = GDImport.gdImageGdPtr( this.Handle, out len);
							break;
						case FileType.Xbm:
						case FileType.Xpm:
							throw new ApplicationException( type + " not implemented." );
						default:
							throw new ApplicationException( type + " is an unknown file type." );
					}

					if( imgDataHandle == IntPtr.Zero )
						throw new ApplicationException( "function Save(FileType type, Stream outstream, int parameter )" );

					byte[] imgData = new byte[len];
				
					Marshal.Copy(imgDataHandle, imgData, 0 , len);
					MemoryStream img = new MemoryStream(imgData);
				
					int BUFFER = 4096;
					byte[] buffer = new byte[BUFFER];
					int numBytes;

					while((numBytes = img.Read(buffer,0,BUFFER))>0)   
						outstream.Write(imgData,0,len);
				}
				finally
				{
					// 2004-01-10 - Must free memory!! - Kevin Tam
					if (imgDataHandle != IntPtr.Zero)
						GDImport.gdFree(imgDataHandle);
				}
			}
			catch(Exception e)
			{
				throw new ApplicationException( "function Save(FileType type, Stream outstream, int parameter ) failed",e );
			}
			return true;
	   	} 

		public void GifAnimBegin(Stream sout)
		{
			GifAnimBegin(sout, true, -1);
		}

		public void GifAnimBegin(Stream sout, bool globalCm)
		{
			GifAnimBegin(sout, globalCm, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sout"></param>
		/// <param name="globalCm"></param>
		/// <param name="loops">Netscape 2/0 extension for animation loop count. 0 - infinite, -1 for not used. -1 is default</param>
		public void GifAnimBegin(Stream sout, bool globalCm, int loops)
		{
			int size;
			using (GDIntPtr rawdata = GDImport.gdImageGifAnimBeginPtr(this.Handle, out size, BoolToInt(globalCm), loops))
			{
				if( rawdata.IsNull)
					throw new ApplicationException( "Function GifAnimBegin failed" );
				byte[] data = new byte[size];
				Marshal.Copy(rawdata, data, 0, size);
				sout.Write(data, 0, size);
			}
		}

		public void GifAnimAdd(Stream sout, int localCm, int leftOfs, int topOfs, int delay, Disposal disposal, GD previm)
		{
			int size;
			IntPtr prevhandle = previm == null ? IntPtr.Zero : previm.handle.Handle;
			using (GDIntPtr rawdata = GDImport.gdImageGifAnimAddPtr(this.Handle, out size, localCm, leftOfs, topOfs, delay, (int)disposal, prevhandle))
			{
				if( rawdata .IsNull )
					throw new ApplicationException( "Function GifAnimAdd failed" );
				byte[] data = new byte[size];
				Marshal.Copy(rawdata, data, 0, size);
				sout.Write(data, 0, size);
			}
		}

		public void GifAnimEnd(Stream sout)
		{
			// Just write a simple semi-colon rather than marshalling a call
			sout.WriteByte((byte)';');
		}
                
	   	~GD()
	   	{
	   		Dispose( false );
	   	}
	
		public void Dispose()
		{
	   		Dispose( true );
	      		GC.SuppressFinalize(this); 
		}   	
		

		protected virtual void Dispose( bool managed )
		{
			if( !this.disposed )
			{
	   			GDImport.gdImageDestroy( this.Handle );
	   			this.disposed = true;
	   		}
	   	}

	   	
	   	public void TrueColorToPalette( int ditherFlag, int colorsWanted )
	   	{
	   		GDImport.gdImageTrueColorToPalette( this.Handle, ditherFlag, colorsWanted );
	   	}
	   	
		public void CheckDisposed()
		{
			if( this.disposed )
				throw new ApplicationException( "It has been disposed already." );
		}
		
		internal HandleRef GetHandle()
		{		
			return this.handle;
		}
		#endregion
	   	
		#region Queries
		/***********************************************************************************\
	   	 GD Query
	   	\***********************************************************************************/

	   	public void GetClip( ref int x1, ref int y1, ref int x2, ref int y2 )
		{
			GDImport.gdImageGetClip( this.Handle, ref x1, ref y1, ref x2, ref y2 );
		}

	   	public GDColor GetPixel( int x, int y )
	   	{
	   		return new GDColor(GDImport.gdImageGetPixel( this.Handle, x, y ));
	   	}

	   	public int BoundsSafe( int x, int y )
	   	{
	   		return GDImport.gdImageBoundsSafe( this.Handle, x, y );
	   	}
	   	
	   	public int Width
	   	{
			get
			{
				return GetStructInt("sx");
			}
	   	}
	   	
	   	public int Height
	   	{
			get
			{
				return GetStructInt("sy");
			}
	   	}
	   	
	   	public int ColorsTotal()
	   	{
			return GetStructInt("colorsTotal");
	   	}

		public int Alpha( GDColor color ) 
		{
			GDImage imageData = (GDImage) Marshal.PtrToStructure( this.Handle, typeof( GDImage ) );

			if( imageData.trueColor > 0 )
				return ( ( color & 0x7F000000 ) >> 24 );			
				
			return imageData.alpha[color];
		}
		
		public int Red( GDColor color ) 
		{
			GDImage imageData = (GDImage) Marshal.PtrToStructure( this.Handle, typeof( GDImage ) );

			if( imageData.trueColor > 0 )
				return ( ( color & 0xFF0000 ) >> 16 );			
				
			return imageData.red[color];
		}
	   	
	   	public int Green( GDColor color ) 
		{
			GDImage imageData = (GDImage) Marshal.PtrToStructure( this.Handle, typeof( GDImage ) );

			if( imageData.trueColor > 0 )
				return ( ( color & 0x00FF00 ) >> 8 );			
				
			return imageData.green[color];
		}
		
		public int Blue( GDColor color ) 
		{
			GDImage imageData = (GDImage) Marshal.PtrToStructure( this.Handle, typeof( GDImage ) );

			if( imageData.trueColor > 0 )
				return ( color & 0x0000FF );			
				
			return imageData.blue[color];
		}
		
		public bool Interlace
		{
			get
			{
				return IntToBool(GetStructInt("interlace"));
			}
			set
			{
				GDImport.gdImageInterlace( this.Handle, BoolToInt(value) );
			}
		}
		
		public GDColor Transparent
		{
			get
			{
				return (GDColor)GetStructInt("transparent");
			}
		}
		
		public int GetTrueColor() 
		{
			return GetStructInt("trueColor");
		}
		#endregion

		#region Copying/Resizing
		/***********************************************************************************\
	   	 GD Copying/Resizing
	   	\***********************************************************************************/
	   	
	   	public void Copy( GD src, int dstX, int dstY, int srcX, int srcY, int w, int h )
		{
			GDImport.gdImageCopy( this.Handle, src.GetHandle().Handle, dstX, dstY, srcX, srcY, w, h );
		}


		public void CopyResized( GD src, int dstX, int dstY, int srcX, int srcY, int destW, int destH, int srcW, int srcH )
		{
			GDImport.gdImageCopyResized( this.Handle, src.GetHandle().Handle, dstX, dstY, srcX, srcY, destW, destH, srcW, srcH );
		}
		
		public void CopyResampled( GD src, int dstX, int dstY, int srcX, int srcY, int destW, int destH, int srcW, int srcH )
		{
			GDImport.gdImageCopyResampled( this.Handle, src.GetHandle().Handle, dstX, dstY, srcX, srcY, destW, destH, srcW, srcH );
		}
		
		public void CopyRotated( GD src, double dstX, double dstY, int srcX, int srcY, int srcW, int srcH, int angle )
		{
			GDImport.gdImageCopyRotated( this.Handle, src.GetHandle().Handle, dstX, dstY, srcX, srcY, srcW, srcH, angle );
		}

		public void CopyMerge( GD src, int dstX, int dstY, int srcX, int srcY, int w, int h, int pct )
		{
			GDImport.gdImageCopyMerge( this.Handle, src.GetHandle().Handle, dstX, dstY, srcX, srcY, w, h, pct );
		}

		public void PaletteCopy( GD src )
		{
			GDImport.gdImagePaletteCopy( this.Handle, src.GetHandle().Handle );
		}
		

		public void SquareToCircle( int radius )
		{
			GDImport.gdImageSquareToCircle( this.Handle, radius );
		}

		public void Sharpen( int pct )
		{
			GDImport.gdImageSharpen( this.Handle, pct );
		}
		#endregion

		#region Miscellaneous
		/***********************************************************************************\
	   	 GD Miscellaneous
	   	\***********************************************************************************/

		public void Compare( GD handle2 )
		{
			GDImport.gdImageCompare( this.Handle, handle2.Handle );
		}

		public void Resize(int destW, int destH) 
		{
			CheckDisposed();

			//TODO: resize img and replace this.handle with new img
			IntPtr imageHandle;
			imageHandle = GDImport.gdImageCreateTrueColor( destW, destH);
			if( imageHandle == IntPtr.Zero )
				throw new ApplicationException( "ImageCreatefailed." );

			GDImport.gdImageCopyResampled(
				imageHandle, this.Handle, 
				0, 0, 0, 0, 
				destW, destH, this.Width, this.Height);

			GDImport.gdImageDestroy( this.Handle );
			this.handle = new HandleRef( this, imageHandle );
		}
		#endregion

		#region Utility functions

		public IntPtr Handle
		{
			get
			{
				CheckDisposed();
				return this.handle.Handle;
			}
		}

		static private int BoolToInt(bool val)
		{
			return val ? (-1) : 0;
		}

		static private bool IntToBool(int val)
		{
			// API uses (-1) for true and 0 for false
			// Will assume that other values are true
			System.Diagnostics.Debug.Assert(val ==0 || val == -1);
			return !(val==0);
		}

		private int GetStructInt(string field)
		{
			return Marshal.ReadInt32(this.Handle, GDImage.GetOffset(field));
		}
		#endregion
	}
	
}
