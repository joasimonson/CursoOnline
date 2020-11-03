import { Selector } from 'testcafe';
import Page from './page-model/page';

const page = new Page();

fixture('Servidor').page(page.urlBase)

test('Validando funcionamento do servidor', async t => {
    await t.expect(Selector('title').innerText).eql('Matrículas')
})