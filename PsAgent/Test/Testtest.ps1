$ip="10.110.42.112"
$user="service"
$pass="Mi@Dim7T"
$method="get perpetual_stats"
$a=.\ps_vplex_perf.ps1 -vplexip $ip -type "POST" -method $method -username $user -password $pass -rawpost
$a[0]