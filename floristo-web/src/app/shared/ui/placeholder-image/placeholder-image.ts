import { Component, input, computed } from '@angular/core';

@Component({
    selector: 'app-placeholder-image',
    standalone: true,
    templateUrl: './placeholder-image.html',
    styleUrl: './placeholder-image.scss'
})

export class PlaceholderImage {
    readonly label = input.required<string>();
    readonly aspect = input<'square' | 'portrait' | 'landscape' | 'fill'>('portrait');

    readonly initials = computed(() => {
        const label = this.label();
        const parts = label.trim().split(/\s+/);
        if (parts.length === 1)
            return parts[0].charAt(0).toUpperCase();
        return (parts[0].charAt(0) + parts[parts.length - 1].charAt(0)).toUpperCase();
    })

    readonly aspectClass = computed(() => {
        switch (this.aspect()) {
            case 'square': return 'aspect-square';
            case 'portrait': return 'aspect-[3/4]';
            case 'landscape': return 'aspect-[4/3]';
            case 'fill': return 'h-full';
        }
    })
}