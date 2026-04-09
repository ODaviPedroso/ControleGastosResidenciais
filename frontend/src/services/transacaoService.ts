import type { Transacao } from '../types/Transacao';
import api from './api';

export const transacaoService = {
  getAll: () => api.get<Transacao[]>('/api/transacoes').then(r => r.data),
  getById: (id: string) => api.get<Transacao>(`/api/transacoes/${id}`).then(r => r.data),
  criar: (transacao: Omit<Transacao, 'id'>) => api.post<Transacao>('/api/transacoes', transacao).then(r => r.data),
  atualizar: (id: string, transacao: Omit<Transacao, 'id'>) => api.put<Transacao>(`/api/transacoes/${id}`, transacao).then(r => r.data),
  deletar: (id: string) => api.delete(`/api/transacoes/${id}`),
};
