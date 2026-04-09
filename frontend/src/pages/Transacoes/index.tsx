import { useEffect, useState } from 'react';
import { categoriaService } from '../../services/categoriaService';
import { pessoaService } from '../../services/pessoaService';
import { transacaoService } from '../../services/transacaoService';
import type { Categoria } from '../../types/Categoria';
import { Finalidade } from '../../types/Categoria';
import type { Pessoa } from '../../types/Pessoa';
import type { Transacao } from '../../types/Transacao';
import { TipoTransacao } from '../../types/Transacao';

const tipoLabel: Record<number, string> = {
  [TipoTransacao.Despesa]: 'Despesa',
  [TipoTransacao.Receita]: 'Receita',
};

const tipoBadge: Record<number, string> = {
  [TipoTransacao.Despesa]: 'badge badge-despesa',
  [TipoTransacao.Receita]: 'badge badge-receita',
};

const brl = (v: number) => v.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

const empty = { descricao: '', valor: '', tipo: String(TipoTransacao.Despesa), categoriaId: '', pessoaId: '' };

export default function TransacoesPage() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [form, setForm] = useState(empty);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  async function load() {
    const [t, p, c] = await Promise.all([
      transacaoService.getAll(),
      pessoaService.getAll(),
      categoriaService.getAll(),
    ]);
    setTransacoes(t);
    setPessoas(p);
    setCategorias(c);
    // Preenche o primeiro valor dos selects caso estejam vazios
    setForm(f => ({
      ...f,
      pessoaId: f.pessoaId || p[0]?.id || '',
      categoriaId: f.categoriaId || c[0]?.id || '',
    }));
  }

  useEffect(() => { load(); }, []);

  // Filtra categorias compatíveis com o tipo de transação selecionado
  const tipoAtual = Number(form.tipo);
  const categoriasFiltradas = categorias.filter(c =>
    c.finalidade === Finalidade.Ambas ||
    (tipoAtual === TipoTransacao.Despesa && c.finalidade === Finalidade.Despesa) ||
    (tipoAtual === TipoTransacao.Receita && c.finalidade === Finalidade.Receita)
  );

  function handleTipoChange(e: React.ChangeEvent<HTMLSelectElement>) {
    // Ao mudar o tipo, reseta a categoria para evitar seleção incompatível
    setForm(f => ({ ...f, tipo: e.target.value, categoriaId: '' }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setLoading(true);
    try {
      await transacaoService.criar({
        descricao: form.descricao,
        valor: Number(form.valor),
        tipo: Number(form.tipo) as TipoTransacao,
        categoriaId: form.categoriaId,
        pessoaId: form.pessoaId,
      });
      setForm(f => ({ ...empty, pessoaId: f.pessoaId, tipo: f.tipo }));
      await load();
    } catch (err: unknown) {
      const msg = err instanceof Error ? err.message : '';
      // Tenta extrair a mensagem de erro da resposta da API
      setError(msg || 'Erro ao criar transação.');
    } finally {
      setLoading(false);
    }
  }

  const pessoaMap = Object.fromEntries(pessoas.map(p => [p.id, p.nome]));
  const categoriaMap = Object.fromEntries(categorias.map(c => [c.id, c.descricao]));

  return (
    <div>
      <h1>Transações</h1>

      <div className="form-card">
        <h2>Nova Transação</h2>
        {error && <p className="msg-error">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group" style={{ flex: '2 1 220px' }}>
              <label>Descrição</label>
              <input
                required
                maxLength={400}
                placeholder="Ex: Supermercado"
                value={form.descricao}
                onChange={e => setForm(f => ({ ...f, descricao: e.target.value }))}
              />
            </div>
            <div className="form-group" style={{ flex: '0 0 130px' }}>
              <label>Valor (R$)</label>
              <input
                required
                type="number"
                min={0.01}
                step={0.01}
                placeholder="0,00"
                value={form.valor}
                onChange={e => setForm(f => ({ ...f, valor: e.target.value }))}
              />
            </div>
            <div className="form-group" style={{ flex: '0 0 140px' }}>
              <label>Tipo</label>
              <select value={form.tipo} onChange={handleTipoChange}>
                <option value={TipoTransacao.Despesa}>Despesa</option>
                <option value={TipoTransacao.Receita}>Receita</option>
              </select>
            </div>
            <div className="form-group">
              <label>Categoria</label>
              <select
                required
                value={form.categoriaId}
                onChange={e => setForm(f => ({ ...f, categoriaId: e.target.value }))}
              >
                <option value="">Selecione...</option>
                {categoriasFiltradas.map(c => (
                  <option key={c.id} value={c.id}>{c.descricao}</option>
                ))}
              </select>
            </div>
            <div className="form-group">
              <label>Pessoa</label>
              <select
                required
                value={form.pessoaId}
                onChange={e => setForm(f => ({ ...f, pessoaId: e.target.value }))}
              >
                <option value="">Selecione...</option>
                {pessoas.map(p => (
                  <option key={p.id} value={p.id}>{p.nome}</option>
                ))}
              </select>
            </div>
            <div className="form-actions">
              <button className="btn btn-primary" type="submit" disabled={loading}>
                Adicionar
              </button>
            </div>
          </div>
        </form>
      </div>

      <div className="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Descrição</th>
              <th>Valor</th>
              <th>Tipo</th>
              <th>Categoria</th>
              <th>Pessoa</th>
            </tr>
          </thead>
          <tbody>
            {transacoes.length === 0 ? (
              <tr><td colSpan={5} className="msg-empty">Nenhuma transação cadastrada.</td></tr>
            ) : transacoes.map(t => (
              <tr key={t.id}>
                <td>{t.descricao}</td>
                <td className={t.tipo === TipoTransacao.Receita ? 'value-positive' : 'value-negative'}>
                  {brl(t.valor)}
                </td>
                <td><span className={tipoBadge[t.tipo]}>{tipoLabel[t.tipo]}</span></td>
                <td>{categoriaMap[t.categoriaId] ?? '—'}</td>
                <td>{pessoaMap[t.pessoaId] ?? '—'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
