import { useEffect, useState } from 'react';
import { relatorioService } from '../../services/relatorioService';
import type { TotaisResponse } from '../../types/Relatorio';

const brl = (v: number) => v.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

function TotaisTable({ titulo, dados }: { titulo: string; dados: TotaisResponse | null }) {
  if (!dados) return null;

  return (
    <div style={{ marginBottom: 36 }}>
      <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 12 }}>{titulo}</h2>
      <div className="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Nome</th>
              <th>Receitas</th>
              <th>Despesas</th>
              <th>Saldo</th>
            </tr>
          </thead>
          <tbody>
            {dados.itens.length === 0 ? (
              <tr><td colSpan={4} className="msg-empty">Sem dados.</td></tr>
            ) : dados.itens.map(item => (
              <tr key={item.id}>
                <td>{item.nome}</td>
                <td className="value-positive">{brl(item.totalReceitas)}</td>
                <td className="value-negative">{brl(item.totalDespesas)}</td>
                <td className={item.saldo >= 0 ? 'value-positive' : 'value-negative'}>
                  {brl(item.saldo)}
                </td>
              </tr>
            ))}
          </tbody>
          <tfoot>
            <tr>
              <td>Total Geral</td>
              <td className="value-positive">{brl(dados.totalGeralReceitas)}</td>
              <td className="value-negative">{brl(dados.totalGeralDespesas)}</td>
              <td className={dados.saldoLiquido >= 0 ? 'value-positive' : 'value-negative'}>
                {brl(dados.saldoLiquido)}
              </td>
            </tr>
          </tfoot>
        </table>
      </div>
    </div>
  );
}

export default function RelatoriosPage() {
  const [porPessoa, setPorPessoa] = useState<TotaisResponse | null>(null);
  const [porCategoria, setPorCategoria] = useState<TotaisResponse | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([
      relatorioService.getTotaisPorPessoa(),
      relatorioService.getTotaisPorCategoria(),
    ]).then(([p, c]) => {
      setPorPessoa(p);
      setPorCategoria(c);
    }).finally(() => setLoading(false));
  }, []);

  if (loading) return <p style={{ padding: 24 }}>Carregando...</p>;

  return (
    <div>
      <h1>Relatórios</h1>
      <TotaisTable titulo="Totais por Pessoa" dados={porPessoa} />
      <TotaisTable titulo="Totais por Categoria" dados={porCategoria} />
    </div>
  );
}
