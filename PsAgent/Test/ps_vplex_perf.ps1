#####
#
# 11/15/2011
# Clint Kitson - EMC vSpecialist - @clintonskitson 
# EMC Community Network - Everything VMware at EMC
# https://community.emc.com/community/connect/everything_vmware?view=overview
#
# 11/15/11 Version 1.0 (suggested currently for non-production use and lab use only, provided as-is)
#
# ps_vplex_perf.ps1 - Retrieve performance information from VPlex
#
# PARAMETERS
# -username
# -password
# -vplexip
# -method (get perpetual_stats)= REST call to implement, refer to API docs 
# -type (GET,POST) = refer to API docs
# -raw (optional) = normalize everything to be a metric and a device per line/object out
# -rawpost (optional) = show raw post-processed metric names and metrics per device
# -table = group the raw output by device and director so we have many metrics on one line
# -view (optional) (directors,cache,ports,clusters,directory,ramf,rdma,storage-volume) = use a default view that specifies columns to display and widths
# -paint = refresh content to screen
# -sleep = used with -paint as interval between refreshes
#
# EXAMPLES
# List all stats = .\ps_vplex_perf.ps1 -vplexip "ip" -type "POST" -method "get perpetual_stats" -username service -password "pass" -raw | ft *
# Show a view of ports = .\ps_vplex_perf.ps1 -vplexip "ip" -type "POST" -method "get perpetual_stats" -username service -password "pass" -view ports -paint
# Show basic director stats, refreshing top view = .\ps_vplex_perf.ps1 -vplexip "ip" -type "POST" -method "get perpetual_stats" -username service -password "pass" -view directors -paint
#
# Experimental -- need to fix parse-- .\ps_vplex_perf.ps1 -vplexip "ip" -type "POST" -method "report poll-monitors" -username service -password "pass" -raw
#
# EXPECTING
# get perpetual_stats - expects that the stats services are running and it is collecting and placing stats in /var/log/VPlex/cli/*PERP*.log
# report poll-monitors - expects that the activate poll-monitors has been ran
#
#####

param($username = "service",$password = "", $vplexip="",[switch]$debug,$method,$type,[switch]$raw,[switch]$rawpost,[switch]$table,$view,[switch]$paint,$sleep=10)


$spath = (Split-Path -parent $MyInvocation.MyCommand.Definition) + "\ssh_function.ps1"
. $spath

if($view) {
    $table = $true
    $hashView = @{}
    $hashView."directors" = @("name","busy %","fe-ops-act counts","fe-ops-read counts/s","fe-ops-write counts/s","fe-read KB/s","fe-write KB/s","read-lat recent-average us","write-lat recent-average us","be-ops-read counts/s","be-ops-write counts/s","be-read KB/s","be-write KB/s")
    $hashView."cache" = @("name","dirty KB","miss counts/s","rhit counts/s","subpg counts/s")
    $hashView."clusters" = @("name","avg latency us","max latency us","min latency us")
    $hashView."directory" = @("name","ch-remote counts","chk-total counts","dir-total counts","dr-remote counts","ops-local counts/s","ops-rem counts/s")
    $hashView."ramf" = @("name","cur-op counts","exp-op counts/s","exp-rd KB/s","exp-wr KB/s","imp-rd KB/s","imp-wr KB/s")
    $hashView."rdma" = @("name","cur-ops counts","read KB/s","write KB/s")
    $hashView."storage-volume" = @("name","read latency average us","read latency recent-average us","write latency average us","write latency recent-average us")
    if($method -eq "get perpetual_stats") {
        $hashView."ports" = @("name","recv-bytes KB/s","send-bytes KB/s","recv-pckts counts/s","send-pckts counts/s")
    } else {
        $hashView."ports" = @("name","read KB/s","write KB/s")
    }
    $hashView."devices" = @("name","read latency average (us)","read latency recent-average (us)","write latency average (us)","write latency recent-average (us)","read KB/s","write KB/s","ops counts/s")

    $hashViewLookupDevices = @{}
    $hashViewLookupDevices."directors" = [regex]"^director |^fe-director "
    $hashViewLookupDevices."cache" = "cache"
    $hashViewLookupDevices."clusters" = [regex]"^cluster-"
    $hashViewLookupDevices."directory" = [regex]"^directory "
    $hashViewLookupDevices."ramf" = [regex]"^ramf "
    $hashViewLookupDevices."rdma" = [regex]"^rdma "
    $hashViewLookupDevices."storage-volume" = [regex]"^storage-volume "
    if($method -eq "get perpetual_stats") {
        $hashViewLookupDevices."ports" = [regex]"^\w\d{1,2}-\w\w\d\d"
    } else {
        $hashViewLookupDevices."ports" = [regex]"^fe-prt|^be-prt"
    }
    $hashViewLookupDevices."devices" = [regex]""    
}

if($debug) { $global:debugon = $true } else { $global:debugon = $false }
remove-variable debug
$debug = @() 

function format_out {
    param([array]$arrOut)
    $arrObj = $arrOut | where{$_.metric_type -notmatch '<|>'} | group device,director | %{
        $tmpObj = new-object -type psobject 
        $tmpObj | Add-member Noteproperty -name Name -value ($_.name -split ", " -join " ")
        $_.group | %{
                $tmpObj | Add-member Noteproperty -name ($_.metric+" "+$_.metric_type) -value $_.value
        }
        $tmpObj
    }

    [array]$tmpSelect = $arrObj| %{ $_.psobject.properties | %{ $_.name } } | select -unique | %{ $_.tostring() }
    if($view) { 
        $tmpSelect = $hashView.$view 
        $tmpDevices = $hashViewLookupDevices.$view
    }
    $tmpFormat = $tmpSelect | %{ @{e=$_.tostring();width=if($_ -eq "name") {if($view -eq "devices") { 40} else {20}}else{7}}}
        
    if($table) { 
        if($paint) { clear; }
        $arrObj | sort name | select $tmpSelect | where {$_.name -match $tmpDevices} | ft $tmpFormat 
    } else { $arrObj | sort name | select $tmpSelect }
    
}

function format_out2 {
    param([array]$arrOut)
    $arrObj = $arrOut | where{$_.metric_type -notmatch '<|>'} | group device,director | %{
        $tmpObj = new-object -type psobject 
        $tmpObj | Add-member Noteproperty -name Name -value ($_.name -split ", " -join " ")
        $_.group | %{
                $tmpObj | Add-member Noteproperty -name ($_.metric+" "+$_.metric_type) -value $_.value
        }
        $tmpObj
    }
    $arrObj   
}


function execp_ssh ($tmpCmd) {
    $expect = "Linux svpg-vplex-|Linux Mana"
    $tmpCmd=$tmpCmd.replace('"','\"')
    send-ssh ('cmd="'+$tmpCmd+'"')
    [array]$tmpOut = invoke-ssh 'echo $cmd |sh;uname -a' $expect
    $tmpStartLine = $tmpOut | select-string -pattern ";uname -a" | %{ $_.linenumber } | select -last 1
    $tmpEndLine = ($tmpOut | select-string -pattern $expect | %{ $_.linenumber } | select -first 1) - 2
    [array]$debug = "***********************************************************************************"
    $debug += $tmpOut | %{ $_ }
    $debug += "***********************************************************************************"
    $debug += ([string]$tmpStartLine+" "+$tmpEndLine)
    if(!$tmpStartLine) { $tmpStartLine = 0 }
    if(!$tmpEndLine) { $tmpEndLine = $tmpOut.count-1 }
    $debug +=  ([string]$tmpStartLine+" "+$tmpEndLine)
        
    $debug +=  "***********************************************************************************"
    $debug +=  $tmpOut[$tmpStartLine]
    $debug +=  $tmpOut[$tmpEndLine]
    $debug +=  "***********************************************************************************"
    if($global:debugon) { write-host $debug;remove-sshsession;exit }
    $tmpOut[($tmpStartLine)..($tmpEndLine)]
}

function proc_csv2 {
    param([array]$csvdata,$file)
    [array]$cols1 = $csvdata[0] -replace "`r","" -split ','
    [array]$cols2 = $csvdata[-1] -replace "`r","" -split ','
    $tmpCols = @()
    0..($cols2.count-1) | %{
        $col = $_
        $arrReg = @()
        # check this regex, matching too much | where {$_ -match 'be-prt'}
        $arrReg += [regex]"^(?<device2>.*-prt)\.(?<metric>.*) (?<device>.*) \((?<metric_type>.*)\)$"
        $arrReg += [regex]"^virtual-volume\.(?<metric>.*) (?<device>.*) \((?<metric_type>.*)\)$"
        $arrReg += [regex]"^virtual-volume\.(?<metric>.*) (?<device>.*) (?<metric_type>.* .*)$"
        $arrReg += [regex]"^(?<device>fe-director)\.(?<metric>.*) \((?<metric_type>.*)\)$"
        $arrReg += [regex]"^storage-volume\.(?<metric>.*) (?<device>.*) (?<metric_type>.* .*)$"
        $arrReg += [regex]"^(?<device>storage-volume)\.(?<metric>.*) \((?<metric_type>.*)\)$"
        
        $arrReg += [regex]"^(?<device_group>.*)\.(?<metric>.*) (?<device>.*) (?<metric_type>.* .*)$"
        $arrReg += [regex]"^(?<device_group>.*)\.(?<metric>.*) (?<device>.*) \((?<metric_type>.*)\)$"
        $arrReg += [regex]"^(?<device>.*)\.(?<metric>.*) \((?<metric_type>.*)\)$"
        $arrReg += [regex]"^(?<device>.*)\.(?<metric>.*) (?<metric_type>.* .*)$"
        $arrReg += [regex]"^(?<metric>.*) (?<device>.*) \((?<metric_type>.*)\)$"

        :loop1 foreach ($reg in $arrReg) { 
            if($reg.match($cols1[$col]).success -eq $true) {
                $cols1[$col] -match $reg | out-null
                #write ($cols1[$col] + " " + $reg.tostring())
                $tmpMetric = $matches.metric -replace "per-storage-volume-","" -replace "-lat$|-latency"," latency"
                1 | select @{n="device";e={if($matches.device2){$matches.device2+" "+$matches.device}else{$matches.device}}},@{n="director";e={$file.replace("director-","")}},@{n="metric";e={if($matches.metric_type -match "^<|^>") { $tmpMetric.split('-')[0]+" totalops" } else { $tmpMetric}}},@{n="value";e={if($cols2[$col] -eq "no data") {""}else {[double]$cols2[$col]}}},@{n="metric_type";e={$matches.metric_type}}
                break loop1
            }
        }
        if($reg.match($cols1[$col]).success -ne $true) { }#write-host ($cols1[$col]) }
    }
}

$assembly = [Reflection.Assembly]::LoadWithPartialName("System.Web.Extensions")
$transmogrifer = New-Object System.Web.Script.Serialization.JavaScriptSerializer

function vplex_rest {
param($method,$vplexip,$username,$password,$type)
    $url="https://$vplexip/vplex/$method" 
    $netAssembly = [Reflection.Assembly]::GetAssembly([System.Net.Configuration.SettingsSection])
    IF($netAssembly) {
        $bindingFlags = [Reflection.BindingFlags] "Static,GetProperty,NonPublic"
        $settingsType = $netAssembly.GetType("System.Net.Configuration.SettingsSectionInternal")
        $instance = $settingsType.InvokeMember("Section", $bindingFlags, $null, $null, @())
        if($instance) {
            $bindingFlags = "NonPublic","Instance"
            $useUnsafeHeaderParsingField = $settingsType.GetField("useUnsafeHeaderParsing", $bindingFlags)
            if($useUnsafeHeaderParsingField) {
                $useUnsafeHeaderParsingField.SetValue($instance, $true) | out-null
            }
        }
    }
    [System.Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
    [System.Net.ServicePointManager]::Expect100Continue = $false

    $webclient = New-Object System.Net.WebClient
    $webclient.Headers.Add("Content-Type","test/json")
    $webclient.Headers.Add("Username",$username)
    $webclient.Headers.Add("Password",$password)

    if($type -eq "GET") {
        $webclient.DownloadString($url) 
    }elseif($type -eq "POST") {
        $webclient.UploadString($url,"POST","") 
    }
}

function cust_sleep {
param($sleep)
    1..($sleep) | %{
        sleep 1
        if ($Host.UI.RawUI.KeyAvailable -and (3 -eq [int]$Host.UI.RawUI.ReadKey("AllowCtrlC,IncludeKeyUp,NoEcho").Character)) { [console]::TreatControlCAsInput = $false;remove-sshsession;write "exiting cleanly and closing SSH session";exit }  
    }
}

if($paint) {
    [console]::TreatControlCAsInput = $true
}

if($method -eq "get perpetual_stats") {
    New-SshSession $username $password $vplexip
    for($x) { 
        [array]$tmpFiles = execp_ssh ("ls /var/log/VPlex/cli | grep PERP | grep '.log$'")
        [array]$arrOut = $tmpFiles | %{
            $fn = $_
            #$fn
            [array]$tmpSshOut = execp_ssh ("head -1 /var/log/VPlex/cli/"+$fn+";tail -1 /var/log/VPlex/cli/"+$fn)
            proc_csv2 -csvdata $tmpSshOut -file $_.split('_')[0]   
        }
        if(!$raw -and !$rawpost) { format_out $arrOut }elseif($rawpost){format_out2 $arrOut} else { $arrOut }
        if(!$paint) { break }
        cust_sleep -sleep $sleep
    }
    remove-sshsession
    exit
}


$tmpHttp = vplex_rest -method $method -vplexip $vplexip -username $username -password $password -type $type
$tmpOut = $transmogrifer.DeserializeObject($tmpHttp)

if(!$tmpOut.response.context) {
    if($method -eq "report poll-monitors") {
        [array]$arrFiles = $tmpOut.response."custom-data" | where {$_} | %{ $_ -split "\n" } | where {$_ -notmatch 'session.log'} | %{ ($_ -replace "\.$","" -split " ")[-1] }
        New-SshSession $username $password $vplexip
        for($x) {
            [array]$arrOut = $arrFiles | where { $_ } | %{ 
                #write-host $_
                $fn = $_.split("_")[1]+"_"+$_.split("_")[0]+".csv"
                [array]$tmpSshOut = execp_ssh ("head -1 /var/log/VPlex/cli/reports/"+$fn+";tail -1 /var/log/VPlex/cli/reports/"+$fn)
                #$tmpSshOut
                proc_csv2 -csvdata $tmpSshOut -file $_.split('_')[0]
            }
            if(!$raw -and !$rawpost) { format_out $arrOut }elseif($rawpost){format_out2 $arrOut} else { $arrOut }
            if(!$paint) { break }
            cust_sleep -sleep $sleep
        }
        remove-sshsession
    } else {
        $tmpOut.response."custom-data" -replace "\[|\]","" -split ", " | sort
    }
} else {
    if($tmpOut.response.context) {
        $tmpOut.response.context | %{ $_.children }
        [console]::TreatControlCAsInput = $false;
        exit
    } 
}

[console]::TreatControlCAsInput = $false;
