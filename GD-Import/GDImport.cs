/*************************************************************************************************************************\

Author:		Mircea-Cristian Racasan <darx_kies@gmx.net>
Copyright: 	2005 by Mircea-Cristian Racasan

This program is free software; you can redistribute it and/or modify it under the terms of the 
GNU General public License as published by the Free Software Foundation; either version 2 of the License, 
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
See the GNU General public License for more details.

You should have received a copy of the GNU General public License along with this program; 
if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

\*************************************************************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace Ntx.GD
{
	/// <summary>
	/// GD APIs
	/// </summary>
	public class GDImport
	{
		#region GD API declarations
		//DEFINE WINDOWS if you need it...            
#if WINDOWS
		public const string LIBC = "msvcrt.dll";
        public const string LIBGD = "bgd.dll";

		[DllImport( LIBGD, EntryPoint = "#16" )]
		public static extern void 		gdFree( IntPtr handle);
		[DllImport( LIBGD, EntryPoint = "#24" )]
		public static extern int 		gdImageColorAllocate( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD, EntryPoint = "#25" )]
		public static extern int 		gdImageColorAllocateAlpha( IntPtr handle, int r, int g, int b, int a );
		[DllImport( LIBGD, EntryPoint = "#29" )]
		public static extern void 		gdImageColorDeallocate( IntPtr handle, int color );
		[DllImport( LIBGD, EntryPoint = "#26" )]
		public static extern int 		gdImageColorClosest( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD, EntryPoint = "#27" )]
		public static extern int 		gdImageColorClosestAlpha( IntPtr handle, int r, int g, int b, int a );
		[DllImport( LIBGD, EntryPoint = "#28" )]
		public static extern int 		gdImageColorClosestHWB( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD, EntryPoint = "#30" )]
		public static extern int 		gdImageColorExact( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD, EntryPoint = "#32" )]
		public static extern int 		gdImageColorResolve( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD, EntryPoint = "#33" )]
		public static extern int 		gdImageColorResolveAlpha( IntPtr handle, int r, int g, int b, int a );
		[DllImport( LIBGD, EntryPoint = "#34" )]
		public static extern void 		gdImageColorTransparent( IntPtr handle, int color );
		[DllImport( LIBGD, EntryPoint = "#117" )]
		public static extern void 		gdImageSetPixel( IntPtr handle, int x, int y, int color );
		[DllImport( LIBGD, EntryPoint = "#100" )]
		public static extern void 		gdImageLine( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD, EntryPoint = "#69" )]
		public static extern void 		gdImageDashedLine( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD, EntryPoint = "#111" )]
		public static extern void 		gdImageRectangle( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD, EntryPoint = "#76" )]
		public static extern void 		gdImageFilledRectangle( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD, EntryPoint = "#110" )]
		public static extern void 		gdImagePolygon( IntPtr handle, int[] points, int pointsTotal, int color );
		[DllImport( LIBGD, EntryPoint = "#75" )]
		public static extern void 		gdImageFilledPolygon( IntPtr handle, int[] points, int pointsTotal, int color );
		[DllImport( LIBGD, EntryPoint = "#20" )]
		public static extern void 		gdImageArc( IntPtr handle, int cx, int cy, int w, int h, int s, int e, int color );
		[DllImport( LIBGD, EntryPoint = "#73" )]
		public static extern void 		gdImageFilledArc( IntPtr handle, int cx, int cy, int w, int h, int s, int e, int color, int style );
		[DllImport( LIBGD, EntryPoint = "#74" )]
		public static extern void 		gdImageFilledEllipse( IntPtr handle, int cx, int cy, int w, int h, int color );
		[DllImport( LIBGD, EntryPoint = "#72" )]
		public static extern void 		gdImageFillToBorder( IntPtr handle, int x, int y, int border, int color );
		[DllImport( LIBGD, EntryPoint = "#71" )]
		public static extern void 		gdImageFill( IntPtr handle, int x, int y, int color );
		[DllImport( LIBGD, EntryPoint = "#113" )]
		public static extern void 		gdImageSetAntiAliased( IntPtr handle, int c );
		[DllImport( LIBGD, EntryPoint = "#114" )]
		public static extern void 		gdImageSetAntiAliasedDontBlend( IntPtr handle, int c );
		[DllImport( LIBGD, EntryPoint = "#115" )]
		public static extern void 		gdImageSetBrush( IntPtr handle, HandleRef brushHandle );
		[DllImport( LIBGD, EntryPoint = "#120" )]
		public static extern void 		gdImageSetTile( IntPtr handle, HandleRef tileHandle );
		[DllImport( LIBGD, EntryPoint = "#118" )]
		public static extern void 		gdImageSetStyle( IntPtr handle, int[] style, int styleLength );
		[DllImport( LIBGD, EntryPoint = "#119" )]
		public static extern void 		gdImageSetThickness( IntPtr handle, int thickness );
		[DllImport( LIBGD, EntryPoint = "#19" )]
		public static extern void 		gdImageAlphaBlending( IntPtr handle, int blending );
		[DllImport( LIBGD, EntryPoint = "#112" )]
		public static extern void 		gdImageSaveAlpha( IntPtr handle, int saveFlag );
		[DllImport( LIBGD, EntryPoint = "#9" )]
		public static extern IntPtr 	gdFontGetSmall();
		[DllImport( LIBGD, EntryPoint = "#7" )]
		public static extern IntPtr 	gdFontGetLarge();
		[DllImport( LIBGD, EntryPoint = "#8" )]
		public static extern IntPtr 	gdFontGetMediumBold();
		[DllImport( LIBGD, EntryPoint = "#6" )]
		public static extern IntPtr 	gdFontGetGiant();
		[DllImport( LIBGD, EntryPoint = "#10" )]
		public static extern IntPtr 	gdFontGetTiny();
		[DllImport( LIBGD, EntryPoint = "#22" )]
		public static extern void 		gdImageChar( IntPtr handle, HandleRef fontHandle, int x, int y, int c, int color );
		[DllImport( LIBGD, EntryPoint = "#23" )]
		public static extern void 		gdImageCharUp( IntPtr handle, HandleRef fontHandle, int x, int y, int c, int color );
		[DllImport( LIBGD, EntryPoint = "#124" )]
		public static extern void 		gdImageString( IntPtr handle, HandleRef fontHandle, int x, int y, string message, int color );
		[DllImport( LIBGD, EntryPoint = "#130" )]
		public static extern void 		gdImageStringUp( IntPtr handle, HandleRef fontHandle, int x, int y, string message, int color );
		[DllImport( LIBGD, EntryPoint = "#125" )]
		public static extern string		gdImageStringFT( IntPtr handle, int[] brect, int fg, string fontname, double ptsize, double angle, int x, int y, string message );
		[DllImport( LIBGD, EntryPoint = "#126" )]
		public static extern string		gdImageStringFTCircle( IntPtr handle, int cx, int cy, double radius, double textRadius, double fillPortion, string font, double points, string top, string bottom, int fgcolor );
		[DllImport( LIBGD, EntryPoint = "#42" )]
		public static extern IntPtr 	gdImageCreate( int width, int height );
		[DllImport( LIBGD, EntryPoint = "#68" )]
		public static extern IntPtr 	gdImageCreateTrueColor( int width, int height );
		[DllImport( LIBGD, EntryPoint = "#55" )]
		public static extern IntPtr  	gdImageCreateFromJpeg( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#58" )]
		public static extern IntPtr  	gdImageCreateFromPng( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#49" )]
		public static extern IntPtr  	gdImageCreateFromGd( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#43" )]
		public static extern IntPtr  	gdImageCreateFromGd2( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#62" )]
		public static extern IntPtr  	gdImageCreateFromWBMP( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#65" )]
		public static extern IntPtr  	gdImageCreateFromXbm( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#66" )]
		public static extern IntPtr  	gdImageCreateFromXpm( IntPtr fileHandle );
 		[DllImport( LIBGD, EntryPoint = "#52" )]
 		public static extern IntPtr 	gdImageCreateFromGif( IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#103" )]
		public static extern void 		gdImagePng( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#106" )]
		public static extern void 		gdImagePngEx( IntPtr handle, IntPtr fileHandle, int level );
		[DllImport( LIBGD, EntryPoint = "#132" )]
		public static extern void 		gdImageWBMP( IntPtr handle, int fg, IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#97" )]
		public static extern void 		gdImageJpeg( IntPtr handle, IntPtr fileHandle, int quality );
		[DllImport( LIBGD, EntryPoint = "#79" )]
		public static extern void 		gdImageGd( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#77" )]
		public static extern void 		gdImageGd2( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#84" )]
		public static extern void 		gdImageGif( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD, EntryPoint = "#99" )]
		public static extern IntPtr		gdImageJpegPtr(IntPtr handle, out int size, int quality);
		[DllImport( LIBGD, EntryPoint = "#107" )]
		public static extern IntPtr		gdImagePngPtr(IntPtr handle, out int size);
		[DllImport( LIBGD, EntryPoint = "#134" )]
		public static extern IntPtr		gdImageWBMPPtr(IntPtr handle, out int size);
		[DllImport( LIBGD, EntryPoint = "#80" )]
		public static extern IntPtr		gdImageGdPtr(IntPtr handle, out int size);
		[DllImport( LIBGD, EntryPoint = "#78" )]
		public static extern IntPtr		gdImageGd2Ptr(IntPtr handle, int chunkSize, int fmt, out int size);
		[DllImport( LIBGD, EntryPoint = "#90" )]
		public static extern IntPtr		gdImageGifAnimBeginPtr(IntPtr handle, out int size, int GlobalCM, int Loops);
		[DllImport( LIBGD, EntryPoint = "#87" )]
		public static extern IntPtr		gdImageGifAnimAddPtr(IntPtr handle, out int size, int LocalCM, int LeftOfs, int TopOfs, int Delay, int Disposal, IntPtr previm);
		[DllImport( LIBGD, EntryPoint = "#93" )]
		public static extern IntPtr		gdImageGifAnimEndPtr(out int size);
		[DllImport( LIBGD, EntryPoint = "#70" )]
		public static extern void 		gdImageDestroy( IntPtr handle );
		[DllImport( LIBGD, EntryPoint = "#131" )]
		public static extern void 		gdImageTrueColorToPalette( IntPtr handle, int ditherFlag, int colorsWanted );
		[DllImport( LIBGD, EntryPoint = "#81" )]
		public static extern void 		gdImageGetClip( IntPtr handle, ref int x1, ref int y1, ref int x2, ref int y2 );
		[DllImport( LIBGD, EntryPoint = "#82" )]
		public static extern int 		gdImageGetPixel( IntPtr handle, int x, int y );
		[DllImport( LIBGD, EntryPoint = "#21" )]
		public static extern int 		gdImageBoundsSafe( IntPtr handle, int x, int y );
		[DllImport( LIBGD, EntryPoint = "#36" )]
		public static extern void 		gdImageCopy( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int w, int h );
		[DllImport( LIBGD, EntryPoint = "#40" )]
		public static extern void 		gdImageCopyResized( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int destW, int destH, int srcW, int srcH );
		[DllImport( LIBGD, EntryPoint = "#39" )]
		public static extern void 		gdImageCopyResampled( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int destW, int destH, int srcW, int srcH );
		[DllImport( LIBGD, EntryPoint = "#41" )]
		public static extern void 		gdImageCopyRotated( IntPtr dst, IntPtr src, double dstX, double dstY, int srcX, int srcY, int srcW, int srcH, int angle );
		[DllImport( LIBGD, EntryPoint = "#37" )]
		public static extern void 		gdImageCopyMerge( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int w, int h, int pct );
		[DllImport( LIBGD, EntryPoint = "#102" )]
		public static extern void 		gdImagePaletteCopy( IntPtr dst, IntPtr src );
		[DllImport( LIBGD, EntryPoint = "#122" )]
		public static extern void 		gdImageSquareToCircle( IntPtr handle, int radius );
		[DllImport( LIBGD, EntryPoint = "#121" )]
		public static extern void 		gdImageSharpen( IntPtr handle, int pct );
		[DllImport( LIBGD, EntryPoint = "#35" )]
		public static extern int 		gdImageCompare( IntPtr handle1, IntPtr handle2 );
		[DllImport( LIBGD, EntryPoint = "#96" )]
		public static extern void		gdImageInterlace( IntPtr handle, int interlace );
		[DllImport( LIBGD, EntryPoint = "#116" )]
		public static extern void 		gdImageSetClip( IntPtr handle, int x1, int y1, int x2, int y2 );
#else   // std posix
		public const string LIBC = "libc.so.6";
		public const string LIBGD = "libgd.so";

		[DllImport( LIBGD )]
		public static extern void 		gdFree( IntPtr handle);
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorAllocate( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorAllocateAlpha( IntPtr handle, int r, int g, int b, int a );
		[DllImport( LIBGD )]
		public static extern void 		gdImageColorDeallocate( IntPtr handle, int color );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorClosest( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorClosestAlpha( IntPtr handle, int r, int g, int b, int a );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorClosestHWB( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorExact( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorResolve( IntPtr handle, int r, int g, int b );
		[DllImport( LIBGD )]
		public static extern int 		gdImageColorResolveAlpha( IntPtr handle, int r, int g, int b, int a );
		[DllImport( LIBGD )]
		public static extern void 		gdImageColorTransparent( IntPtr handle, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetPixel( IntPtr handle, int x, int y, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageLine( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageDashedLine( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageRectangle( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageFilledRectangle( IntPtr handle, int x1, int y1, int x2, int y2, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImagePolygon( IntPtr handle, int[] points, int pointsTotal, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageFilledPolygon( IntPtr handle, int[] points, int pointsTotal, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageArc( IntPtr handle, int cx, int cy, int w, int h, int s, int e, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageFilledArc( IntPtr handle, int cx, int cy, int w, int h, int s, int e, int color, int style );
		[DllImport( LIBGD )]
		public static extern void 		gdImageFilledEllipse( IntPtr handle, int cx, int cy, int w, int h, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageFillToBorder( IntPtr handle, int x, int y, int border, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageFill( IntPtr handle, int x, int y, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetAntiAliased( IntPtr handle, int c );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetAntiAliasedDontBlend( IntPtr handle, int c );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetBrush( IntPtr handle, HandleRef brushHandle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetTile( IntPtr handle, HandleRef tileHandle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetStyle( IntPtr handle, int[] style, int styleLength );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetThickness( IntPtr handle, int thickness );
		[DllImport( LIBGD )]
		public static extern void 		gdImageAlphaBlending( IntPtr handle, int blending );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSaveAlpha( IntPtr handle, int saveFlag );
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdFontGetSmall();
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdFontGetLarge();
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdFontGetMediumBold();
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdFontGetGiant();
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdFontGetTiny();
		[DllImport( LIBGD )]
		public static extern void 		gdImageChar( IntPtr handle, HandleRef fontHandle, int x, int y, int c, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageCharUp( IntPtr handle, HandleRef fontHandle, int x, int y, int c, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageString( IntPtr handle, HandleRef fontHandle, int x, int y, string message, int color );
		[DllImport( LIBGD )]
		public static extern void 		gdImageStringUp( IntPtr handle, HandleRef fontHandle, int x, int y, string message, int color );
		[DllImport( LIBGD )]
		public static extern string		gdImageStringFT( IntPtr handle, int[] brect, int fg, string fontname, double ptsize, double angle, int x, int y, string message );
		[DllImport( LIBGD )]
		public static extern string		gdImageStringFTCircle( IntPtr handle, int cx, int cy, double radius, double textRadius, double fillPortion, string font, double points, string top, string bottom, int fgcolor );
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdImageCreate( int width, int height );
		[DllImport( LIBGD )]
		public static extern IntPtr 	gdImageCreateTrueColor( int width, int height );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromJpeg( IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromPng( IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromGd( IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromGd2( IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromWBMP( IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromXbm( IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr  	gdImageCreateFromXpm( IntPtr fileHandle );
		[DllImport( LIBGD )]
 		public static extern IntPtr 	gdImageCreateFromGif( IntPtr fileHandle );
 		[DllImport( LIBGD )]
		public static extern void 		gdImagePng( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern void 		gdImagePngEx( IntPtr handle, IntPtr fileHandle, int level );
		[DllImport( LIBGD )]
		public static extern void 		gdImageWBMP( IntPtr handle, int fg, IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageJpeg( IntPtr handle, IntPtr fileHandle, int quality );
		[DllImport( LIBGD )]
		public static extern void 		gdImageGd( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageGd2( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageGif( IntPtr handle, IntPtr fileHandle );
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageJpegPtr(IntPtr handle, out int size, int quality);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImagePngPtr(IntPtr handle, out int size);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageWBMPPtr(IntPtr handle, out int size);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageGdPtr(IntPtr handle, out int size);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageGd2Ptr(IntPtr handle, int chunkSize, int fmt, out int size);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageGifAnimBeginPtr(IntPtr handle, out int size, int GlobalCM, int Loops);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageGifAnimAddPtr(IntPtr handle, out int size, int LocalCM, int LeftOfs, int TopOfs, int Delay, int Disposal, IntPtr previm);
		[DllImport( LIBGD )]
		public static extern IntPtr		gdImageGifAnimEndPtr(out int size);
		[DllImport( LIBGD )]
		public static extern void 		gdImageDestroy( IntPtr handle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageTrueColorToPalette( IntPtr handle, int ditherFlag, int colorsWanted );
		[DllImport( LIBGD )]
		public static extern void 		gdImageGetClip( IntPtr handle, ref int x1, ref int y1, ref int x2, ref int y2 );
		[DllImport( LIBGD )]
		public static extern int 		gdImageGetPixel( IntPtr handle, int x, int y );
		[DllImport( LIBGD )]
		public static extern int 		gdImageBoundsSafe( IntPtr handle, int x, int y );
		[DllImport( LIBGD )]
		public static extern void 		gdImageCopy( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int w, int h );
		[DllImport( LIBGD )]
		public static extern void 		gdImageCopyResized( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int destW, int destH, int srcW, int srcH );
		[DllImport( LIBGD )]
		public static extern void 		gdImageCopyResampled( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int destW, int destH, int srcW, int srcH );
		[DllImport( LIBGD )]
		public static extern void 		gdImageCopyRotated( IntPtr dst, IntPtr src, double dstX, double dstY, int srcX, int srcY, int srcW, int srcH, int angle );
		[DllImport( LIBGD )]
		public static extern void 		gdImageCopyMerge( IntPtr dst, IntPtr src, int dstX, int dstY, int srcX, int srcY, int w, int h, int pct );
		[DllImport( LIBGD )]
		public static extern void 		gdImagePaletteCopy( IntPtr dst, IntPtr src );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSquareToCircle( IntPtr handle, int radius );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSharpen( IntPtr handle, int pct );
		[DllImport( LIBGD )]
		public static extern int 		gdImageCompare( IntPtr handle1, IntPtr handle2 );
		[DllImport( LIBGD )]
		public static extern void		gdImageInterlace( IntPtr handle, int interlace );
		[DllImport( LIBGD )]
		public static extern void 		gdImageSetClip( IntPtr handle, int x1, int y1, int x2, int y2 );
#endif
		#endregion

		#region File Handling
		/***********************************************************************************\
		 File Handling
		\***********************************************************************************/
		[DllImport( LIBC )]
		public static extern IntPtr 	fopen( string filename, string mode );
	   	
		[DllImport( LIBC )]
		public static extern void 		fclose( IntPtr file );
		#endregion
	}
}
