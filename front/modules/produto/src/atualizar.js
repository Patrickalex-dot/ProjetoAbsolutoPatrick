async function getProdutoId(){
    const urlParams = new URLSearchParams(window.location.search);
    let res = await BuscarPorId(urlParams.get('id'));
    PreencherFormulario(res);
}

async function BuscarPorId(id){
    const options = {
        method: 'GET',
        headers:{'content-type':'aplication/json'}        

    };
    const req = await fetch ('https://localhost:44345/produto/BuscarPorId?id='+id,options)
    .then(response =>{
        return response.json()        
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req
}
async function PreencherFormulario(json){

    let dadosForm = document.querySelector('#form');
    let id = dadosForm.querySelector('#id-produto');
    let valor = dadosForm.querySelector('#valor');
    let descricao = dadosForm.querySelector('#descricao');
    

    id.value = json.value.resultado.idProduto;
    valor.value = json.value.resultado.valor;
    descricao.value = json.value.resultado.descricao;
}
async function EnviarApi(viewmodel){
    const options = {
        method:'PUT',
        headers:{'content-type':'application/json'},
        body: JSON.stringify(viewmodel)
    };
    const req = await fetch('https://localhost:44345/produto/atualizar',options)
    .then(response =>{
        response.text()
        .then(data =>{
            return data;
        });
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req
}
async function Atualizar(){
    let id = parseInt(document.querySelector('#id-produto').value);
    console.log(id);
    let valor = document.querySelector('#valor').value;
    console.log(valor);
    let descricao = document.querySelector('#descricao').value;
    console.log(descricao);
    
    let produto = {
        idProduto: id,
        valor : parseFloat(valor),
        descricao,
    };
    let atualizarProdutoViewModel = {
        atualizar : produto
    };
    const options = {    
        method: 'PUT', 
        headers:{'content-type': 'application/json'},       
        body: JSON.stringify(atualizarProdutoViewModel) 
    };
    const req = await fetch('https://localhost:44345/produto/Atualizar',options)
    .then (response =>{
        return response.json();

    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    if(req.value.sucesso){
        alert(req.value.mensagem);
        voltar();
    }
    else{
        alert(req.mensagem);
    }
}
function voltar(){
    window.location.href ='./listar.html';
}
getProdutoId();