import { Selector } from 'testcafe';

export default class Page {
    urlBase: string;
    tituloPagina: Selector;
    
    constructor () {
        this.urlBase = 'https://localhost:5001';
        this.tituloPagina = Selector('title');
    }
}