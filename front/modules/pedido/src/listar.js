async function PreencherTabelaPedido(resposta,limpar){
    let tabela = document.querySelector('#listagem-pedidos');

    if(limpar)
    tabela.innerHTML = '';

    if(!resposta.value.sucesso)
        alert(resposta.value.mensage);
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
                    window.location.href ="atualizar.html?id=" + e.idPedido
                 });


            let botaoRemover = document.createElement('input');
            botaoRemover.value = 'Remover'
            botaoRemover.type = 'button';
            botaoRemover.addEventListener('click',async ()=>{
                     await remover(e.idPedido);
                });
            
            btnsTd.appendChild(botaoEditar);
            btnsTd.appendChild(botaoRemover);
            
            let idInput = document.createElement('input');
            idInput.type = 'hidden';
            let nomeClienteTb = document.createElement('td');
            nomeClienteTb.classList.add('row-cliente-cliente');
            let descricaoProdutoTb = document.createElement('td');
            descricaoProdutoTb.classList.add('row-produto-pedido');
            let nomeMotoristaTb = document.createElement('td');
            nomeMotoristaTb.classList.add('row-motorista-pedido');
            let tipoPagamentoDescricaoTb = document.createElement('td');
            tipoPagamentoDescricaoTb.classList.add('row-tipo-pagamento-pedido')
            let valorTotalTb = document.createElement('td');
            valorTotalTb.classList.add('row-valortotal-pedido');
            let dataHoraEntregaTb = document.createElement('td');
            dataHoraEntregaTb.classList.add('row-datahoraentrega-pedido');
            let placaTb = document.createElement('td');
            placaTb.classList.add('row-placa-pedido');
            let situacaoTb = document.createElement('td');
            situacaoTb.classList.add('row-situacao-pedido');
                
            
            
            
            idInput.value = e.idPedido;
            nomeClienteTb.innerHTML = e.nomeCliente;
            descricaoProdutoTb.innerHTML = e.descricaoProduto;
            nomeMotoristaTb.innerHTML = e.nomeMotorista;
            tipoPagamentoDescricaoTb.innerHTML = e.tipoPagamento;
            valorTotalTb.innerHTML = e.valorTotalPedido;
            dataHoraEntregaTb.innerHTML = e.dataHoraEntrega;
            placaTb.innerHTML = e.placa;
            situacaoTb.innerHTML = e.situacao;

            linha.appendChild(idInput);
            linha.appendChild(nomeClienteTb);
            linha.appendChild(descricaoProdutoTb);
            linha.appendChild(nomeMotoristaTb);
            linha.appendChild(placaTb);
            linha.appendChild(situacaoTb);
            linha.appendChild(dataHoraEntregaTb);
            linha.appendChild(valorTotalTb);
            linha.appendChild(tipoPagamentoDescricaoTb);

            linha.appendChild(btnsTd);
            
            tabela.appendChild(linha);
        })
    }
}

async function ListarPedido(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/pedido/BuscarTodos',options)
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
    const req = await fetch('https://localhost:44345/pedido/Remover?idPedido='+id, options)
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
    let res =await ListarPedido();
    PreencherTabelaPedido(res,false);
})();