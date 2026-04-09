import type { Pessoa } from '../types/Pessoa';
import api from './api';

export const pessoaService = {
  getAll: () => api.get<Pessoa[]>('/api/pessoas').then(r => r.data),
  getById: (id: string) => api.get<Pessoa>(`/api/pessoas/${id}`).then(r => r.data),
  criar: (pessoa: Omit<Pessoa, 'id'>) => api.post<Pessoa>('/api/pessoas', pessoa).then(r => r.data),
  atualizar: (id: string, pessoa: Omit<Pessoa, 'id'>) => api.put<Pessoa>(`/api/pessoas/${id}`, pessoa).then(r => r.data),
  deletar: (id: string) => api.delete(`/api/pessoas/${id}`),
};
