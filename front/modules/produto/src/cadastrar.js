async function capturarDadosProduto(){
    let valor = document.querySelector('#valor').value;
    console.log(valor);
    let descricao = document.querySelector('#descricao').value;
    console.log(descricao);

    let produto = {
        valor: parseFloat(valor),
        descricao,
    };
    let salvarProdutoViewModel = {
        produto,
    };
    console.log(salvarProdutoViewModel);
    
    let response = await EnviarApi(salvarProdutoViewModel);
    console.log(response);
}

async function EnviarApi(viewmodel){
    const options = {
        method: 'POST',
        headers:{'content-type':'application/json'},

        body:JSON.stringify(viewmodel)
    };
    const req = await fetch ('https://localhost:44345/produto/salvar',options)
    .then(response =>{
        response.text()
        .then(data => {
            console.log(data);
            alert(data)
            return data;
        });
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    voltar();
    return req;
}
function voltar(){
    window.location.href ='./listar.html';
 }