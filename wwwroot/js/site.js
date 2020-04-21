// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
const video = Twilio.Video;

let token = "";
function getTwilioToken() {
    $.get("api/twilio/token", (data) => {
        if (data && data.token) {
            token = data.token;
        }
    });
}

async function connect() {
    const options = { audio: true, dominantSpeaker: true, region: "us2", video: true };
    const room = await video.connect(token, options);
}

async function loadCameras() {
    try {
        const cameras = $("#cameras");
        if (!cameras) {
            return;
        }

        await navigator.mediaDevices.getUserMedia({ audio: true, video: true });
        let devices = await navigator.mediaDevices.enumerateDevices();
        if (devices && devices.length) {
            devices.forEach(device => {
                if (!device.label || device.kind !== "videoinput") {
                    return;
                }

                var camera = $("<a>");
                camera.attr("href", "#");
                camera.click(async () => {
                    await startVideo(device.deviceId);
                });
                camera.attr("id", device.deviceId);
                camera.addClass("dropdown-item");
                camera.text(device.label);
                cameras.append(camera);
            });
        }
    } catch (error) {
        console.log(error);
    }
}

loadCameras();

_videoTrack = null;

async function startVideo(deviceId) {
    try {
        const cameraContainer = $("#camera-container");
        if (!cameraContainer) {
            return;
        }

        if (_videoTrack) {
            _videoTrack.detach().forEach(element => element.remove());
        }

        const tracks = await video.createLocalTracks({ audio: true, video: { deviceId } });
        _videoTrack = tracks.find(t => t.kind === 'video');
        const videoElement = _videoTrack.attach();
        cameraContainer.append(videoElement);
    } catch (error) {
        console.log(error);
    }
}