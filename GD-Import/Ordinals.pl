#!/usr/bin/perl
use strict;

# exported func => ordinal
my %exports;

open BGD, "bgd.def";
while (my $line = <BGD>) {
	if ($line =~ /(\w+)(\@\d+)? \@ (\d+)/) {
		$exports{$1} = $3;
		print STDERR "$1 - $3\n";
	}
}
close BGD;

open IN, "GDImport.cs";

my $lastws;
my $skip = 0;
while (my $line = <IN>) {
	if ($line =~ /(\s*)\[DllImport\( LIBGD, EntryPoint/) {
		$lastws = $1;;
	}
	elsif ($line =~ /#else/) {
		$skip = 1;
		print $line
	}
	elsif ($line =~ /static extern \w+\s+(\w+)/) {
		print STDERR "Found $1\n";
		if ($skip == 0)
		{
			if (defined $exports{$1}) {
				print "$lastws\[DllImport( LIBGD, EntryPoint = \"#$exports{$1}\" )]\r\n";
			}
			else {
				print STDERR "WARNING: Unknown function $1\n";
			}
		}
		print $line;
	}
	else {
		print $line;
	}
}

