import BaseService from './baseService';
import Example from '@/models/example';

export class ExampleService extends BaseService {
  constructor() {
    super('example-entities', Example);
  }
}

export default new ExampleService();
