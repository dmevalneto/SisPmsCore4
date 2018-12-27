select 

colaborador.nome as NomeCo, colaborador.telefone as TelCo, colaborador.cpf as CpfCo, colaborador.data_admissao as DataCo, 
setor.nome as NomeSe, setor.cep as CepSe, setor.bairro as BairroSe, setor.logradouro as LogradouroSe, setor.numero as NumeroSe, setor.gestor as GestorSe,
ocorrencia.descricao as DescricaoOco,
cargo.nome as NomeCa,
historico.data as DataHis

from colaborador
inner join setor 
on colaborador.setor_idsetor=setor.idsetor
inner join ocorrencia 
on colaborador.ocorrencia_idocorrencia=ocorrencia.idocorrencia
inner join prestadora_servico 
on colaborador.prestadora_servico_idprestadora_servico = prestadora_servico.idprestadora_servico
inner join cargo 
on colaborador.cargo_idcargo = cargo.idcargo
inner join historico 
on colaborador.idcolaborador = historico.colaborador_idcolaborador