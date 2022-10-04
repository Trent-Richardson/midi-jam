import * as Tone from 'tone'

const synth = new Tone.Synth().toDestination();

export function playNote(index) {

    switch (index) {
        case 0:
            synth.triggerAttackRelease("C4", "8n");
        case 1:
            synth.triggerAttackRelease("C#4", "8n");
        case 2:
            synth.triggerAttackRelease("D4", "8n");
        case 3:
            synth.triggerAttackRelease("D#4", "8n");
        case 4:
            synth.triggerAttackRelease("E4", "8n");
        case 5:
            synth.triggerAttackRelease("F4", "8n");
        case 6:
            synth.triggerAttackRelease("F#4", "8n");
        case 7:
            synth.triggerAttackRelease("G4", "8n");
        case 8:
            synth.triggerAttackRelease("G#4", "8n");
        case 9:
            synth.triggerAttackRelease("A4", "8n");
        case 10:
            synth.triggerAttackRelease("A#4", "8n");
        case 11:
            synth.triggerAttackRelease("B4", "8n");
    }    
}

