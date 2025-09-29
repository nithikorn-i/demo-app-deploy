export interface PagedResult<T> {
  result: T;
  page: number;
  pageSize: number;
  totalCount: number;
}
