import Vue from 'vue';
import { Component } from 'vue-property-decorator'

interface Hotel {
    id: number;
    name: string;
    city: string;
}

@Component
export default class HotelsComponent extends Vue {
    hotels: Hotel[] = [];

    mounted() {
        fetch('/api/hotels')
            .then(response => response.json() as Promise<Hotel[]>)
            .then(data => {
                this.hotels = data;
            });
    }
}
