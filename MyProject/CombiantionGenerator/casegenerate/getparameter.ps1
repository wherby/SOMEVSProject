
#getparameter.ps1

import-module -name esipstoolkit
$cmds = get-command -module esipstoolkit

$output = "c:\users\administrator.sr5dom\desktop\parameter.txt"
if((get-item $output).exists)
{
	remove-item $output
}

$nr_cmds = $cmds.count;
#write-host $nr_cmds

foreach ($cmd in $cmds)
{
	$cmd_name = $cmd.name
	$cmd_syntaxItem = (get-help $cmd_name).syntax.syntaxItem
	$nr_item = $cmd_syntaxItem.count
	$cmd.name + ":" + $nr_item | out-file $output -append
	foreach($item in $cmd_syntaxItem)
	{
		$str_mandatory = ""
		$str_optional = ""
		$str_alternate = ""
		$m_prefix = ""
		$o_prefix = ""
		$a_prefix = ""
		
		foreach($param in $item.parameter)
		{
			#write-host $param.name
			$value = " $" + $param.name
			
			if($param.required -eq $true)
			{
				$str_mandatory += $m_prefix + " -" + $param.name + $value
				$m_prefix = ","
			}
			elseif( $param.name -eq "Confirm")
			{
				$str_mandatory += $m_prefix + " -" + $param.name + " False"
				$m_prefix = ","
			}
			else
			{
				$str_optional += $o_prefix + " -" + $param.name + $value
				$o_prefix = ","
			}
			
			if($param.position -ne "named")
			{
				$str_alternate += $a_prefix + " -" + $param.name + "." + $param.position
				$a_prefix = ","
			}
			
		}
		
		if($str_alternate -ne "")
		{
			"alternate:" + $str_alternate | out-file $output -append
		}
		"mandatory:" + $str_mandatory | out-file $output  -append 
		"optional:" + $str_optional | out-file $output -append
		
		
	}
		
}
$output