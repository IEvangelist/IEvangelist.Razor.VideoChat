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

getTwilioToken();

async function connect() {
    const options = { audio: true, dominantSpeaker: true, region: "us2", video: true };
    const room = await video.connect(token, options);
}
