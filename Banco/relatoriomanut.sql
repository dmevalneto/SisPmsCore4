select 
manutencao.idmanutencao, manutencao.flg, manutencao.data as DataSolicitada,
setor.nome as NomeSe,
item.nome as NomeItem,
historico_manutencao.idhistorico_manutencao, historico_manutencao.os, historico_manutencao.status_manutencao_idstatus_manutencao, historico_manutencao.data as DataAtendida,
status_manutencao.nome as NomeStatus
from manutencao
inner join setor on manutencao.setor_idsetor = setor.idsetor
inner join item on manutencao.item_iditem = item.iditem
inner join historico_manutencao on manutencao.idmanutencao = historico_manutencao.manutencao_idmanutencao
inner join status_manutencao on historico_manutencao.status_manutencao_idstatus_manutencao = status_manutencao.idstatus_manutencao
where setor.idsetor = 17