async function PreencherTabelaProduto(resposta,limpar){
    let tabela = document.querySelector('#listagem-produtos');

    if(limpar)
    tabela.innerHTML = '';

    if(!resposta.value.sucesso)
        alert(resposta.mensage);
    else if(resposta.value.resultado.lenght == 0 ){
        tabela.innerHTML = 'Não há registro para exibir.';
    }
    else {
        resposta.value.resultado.forEach(function(e){
            let linha = document.createElement('tr');
            let btnsTd = document.createElement('td');
            let botaoEditar = document.createElement('input')
            botaoEditar.value ='Editar'
            botaoEditar.type = 'button';
            botaoEditar.addEventListener('click',()=>{
                    window.location.href ="./atualizar.html?id=" + e.idProduto
                 });


            let botaoRemover = document.createElement('input');
            botaoRemover.value = 'Remover'
            botaoRemover.type = 'button';
            botaoRemover.addEventListener('click',async ()=>{
                     await remover(e.idProduto);
                });
            
            btnsTd.appendChild(botaoEditar);
            btnsTd.appendChild(botaoRemover);
            
            let idInput = document.createElement('input');
            idInput.type = 'hidden';
            let valorTb = document.createElement('td');
            valorTb.classList.add('row-nomeCompleto-cliente');
            let descricaoTb = document.createElement('td');
            descricaoTb.classList.add('row-cpf-cliente');
            
            idInput.value = e.idProduto;
            valorTb.innerHTML = e.valor;
            descricaoTb.innerHTML = e.descricao;
            

            linha.appendChild(idInput);
            linha.appendChild(valorTb);
            linha.appendChild(descricaoTb);
            
            
            linha.appendChild(btnsTd);
            
            tabela.appendChild(linha);
        })
    }
}

async function ListarProdutos(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/produto/BuscarTodos',options)
    .then(response=>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req;
}

async function remover (id){

    const options = {
        method : 'DELETE',
        Headers :{'content-type':'aplication/json'}
    };
    const req = await fetch('https://localhost:44345/produto/Remover?id='+id, options)
    .then(response =>{
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
    else {
        alert (req.mensagem);
    }
}
function voltar(){
    window.location.href ='../listar.html';
}
(async() =>{
    let res =await ListarProdutos();
    PreencherTabelaProduto(res,false);
})();