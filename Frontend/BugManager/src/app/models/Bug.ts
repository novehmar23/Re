export interface Bug {
  name: string;
  description: string;
  version: string;
  time: number;
  projectId: number;
  projectName: string;
  id?: number;
  completedById?: number;
  isActive: boolean;
  completedByUsername?: string;
}