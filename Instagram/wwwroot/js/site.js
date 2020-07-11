// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*Like button*/

$(document).ready(function(){
  $("#heart").click(function(){
    if($("#heart").hasClass("liked")){
      $("#heart").html('<i class="fa fa-heart-o" aria-hidden="true"></i>');
      $("#heart").removeClass("liked");
    }else{
      $("#heart").html('<i class="fa fa-heart" aria-hidden="true"></i>');
      $("#heart").addClass("liked");
    }
  });
});
$(".like-btn").click(function (e) {
    var curElement = $(this);
    var liked = $(this).hasClass("liked");
    if (liked == true) curElement.removeClass("liked");
    else curElement.addClass("liked");
});
autosize($(".coment-text-input"));