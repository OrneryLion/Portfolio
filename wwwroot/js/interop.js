 function showPrompt(message) {
     prompt(message, 'Type anything here');
}

window.playVideo = function playVid (url) {
    var video = document.getElementById('video');
    var mp4 = document.getElementById('mp4');
    mp4.src = url;
    video.load();
    video.play();
}