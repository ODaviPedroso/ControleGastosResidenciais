import type { Categoria } from '../types/Categoria';
import api from './api';

export const categoriaService = {
  getAll: () => api.get<Categoria[]>('/api/categorias').then(r => r.data),
  getById: (id: string) => api.get<Categoria>(`/api/categorias/${id}`).then(r => r.data),
  criar: (categoria: Omit<Categoria, 'id'>) => api.post<Categoria>('/api/categorias', categoria).then(r => r.data),
  atualizar: (id: string, categoria: Omit<Categoria, 'id'>) => api.put<Categoria>(`/api/categorias/${id}`, categoria).then(r => r.data),
  deletar: (id: string) => api.delete(`/api/categorias/${id}`),
};
