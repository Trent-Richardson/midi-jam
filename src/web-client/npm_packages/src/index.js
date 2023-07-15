import * as Tone from 'tone'

let synth = new Tone.Synth().toDestination();

export function playNote(index) {

    const now = Tone.now();
    switch (index) {
        case 0:
            synth.triggerAttackRelease("C4", "16n", now + 0.1);
        case 1:
            synth.triggerAttackRelease("C#4", "16n", now + 0.1);
        case 2:
            synth.triggerAttackRelease("D4", "16n", now + 0.1);
        case 3:
            synth.triggerAttackRelease("D#4", "16n", now + 0.1);
        case 4:
            synth.triggerAttackRelease("E4", "16n", now + 0.1);
        case 5:
            synth.triggerAttackRelease("F4", "16n", now + 0.1);
        case 6:
            synth.triggerAttackRelease("F#4", "16n", now + 0.1);
        case 7:
            synth.triggerAttackRelease("G4", "16n", now + 0.1);
        case 8:
            synth.triggerAttackRelease("G#4", "16n", now + 0.1);
        case 9:
            synth.triggerAttackRelease("A4", "16n", now + 0.1);
        case 10:
            synth.triggerAttackRelease("A#4", "16n", now + 0.1);
        case 11:
            synth.triggerAttackRelease("B4", "16n", now + 0.1);
    }
    synth = undefined;
}

