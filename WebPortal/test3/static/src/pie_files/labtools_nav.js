//--Get get the first item after the / in a url 
function get_url_param() {
  var results =  window.location.href;
  if( results == null ) {
    return "";
  }
  else {
    var toptab = results.split('/');
    return toptab[3];
  }
}

//--Observe the url and highlight the current tab
Event.observe(window, 'load',function() {

  var tab_id = get_url_param();

  switch(tab_id) {
  case 'usd_one':
    $('tab1').addClassName('selected_tab');
    break;
  case 'usd_tv':
    $('tab2').addClassName('selected_tab');
    break;
  case 'dashboard':
    $('tab3').addClassName('selected_tab');
    break;
  case 'utilities':
    $('tab4').addClassName('selected_tab');
    break;
  case 'reports':
    $('tab5').addClassName('selected_tab');
    break;
  case 'community':
    $('tab6').addClassName('selected_tab');
    break;
  case 'support':
    $('tab7').addClassName('selected_tab');
    break;
  default:
  }  

});

//Event.observe(window, 'resize', function() {
//  alert('resize');
//}

