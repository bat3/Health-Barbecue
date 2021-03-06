import { ISeries } from '../../models/series'
import {
  SET_CURRENT_SERIES,
  UNSET_CURRENT_SERIES,
  SET_ALL_SERIES,
  FETCH_ALL_SERIES,
  UPDATE_SERIES,
  FETCH_ONE_SERIES,
} from './actionTypes'

interface FetchSeriesAction {
  type: typeof FETCH_ALL_SERIES
}

export interface FetchOneSeriesAction {
  type: typeof FETCH_ONE_SERIES
  id: string
}

interface SetCurrentSeriesAction {
  type: typeof SET_CURRENT_SERIES
  series: ISeries
}
interface UnsetCurrentSeriesAction {
  type: typeof UNSET_CURRENT_SERIES
}

interface SetAllSeriesAction {
  type: typeof SET_ALL_SERIES
  series: ISeries[]
}

export interface UpdateSeriesAction {
  type: typeof UPDATE_SERIES
  series: ISeries
}

export type SeriesActionTypes =
  | UnsetCurrentSeriesAction
  | SetCurrentSeriesAction
  | SetAllSeriesAction
  | FetchSeriesAction
  | FetchOneSeriesAction
  | UpdateSeriesAction

export interface SeriesState {
  series: ISeries | null
  list: ISeries[]
}
