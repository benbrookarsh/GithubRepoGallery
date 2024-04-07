export interface GitHubRepoResult {
  totalCount: number;
  incompleteResults: boolean;
  items: GitHubRepo[];
}

export interface GitHubRepo {
  id: number;
  nodeId: string;
  name: string;
  fullName: string;
  private: boolean;
  owner: Owner;
  htmlUrl: string;
  description?: string; // Assuming description can be optional
  fork: boolean;
  url: string;
  forksUrl: string;
  createdAt: Date;
  updatedAt: Date;
  pushedAt: Date;
  gitUrl: string;
  sshUrl: string;
  cloneUrl: string;
  svnUrl: string;
  homepage?: string; // Assuming homepage can be optional
  size: number;
  stargazersCount: number;
  watchersCount: number;
  language?: string; // Assuming language can be optional
  hasIssues: boolean;
  hasProjects: boolean;
  hasDownloads: boolean;
  hasWiki: boolean;
  hasPages: boolean;
  hasDiscussions: boolean;
  forksCount: number;
  mirrorUrl?: string; // Assuming mirrorUrl can be optional
  archived: boolean;
  disabled: boolean;
  openIssuesCount: number;
  license?: License; // Assuming license can be optional
  allowForking: boolean;
  isTemplate: boolean;
  webCommitSignoffRequired: boolean;
  topics: string[];
  visibility: string;
  forks: number;
  openIssues: number;
  watchers: number;
  defaultBranch: string;
  score: number;
}

export interface Owner {
  login: string;
  id: number;
  nodeId: string;
  avatar_Url: string;
  gravatarId?: string; // Assuming gravatarId can be optional
  url: string;
  htmlUrl: string;
  followersUrl: string;
  followingUrl: string;
  gistsUrl: string;
  starredUrl: string;
  subscriptionsUrl: string;
  organizationsUrl: string;
  reposUrl: string;
  eventsUrl: string;
  receivedEventsUrl: string;
  type: string;
  siteAdmin: boolean;
}

export interface License {
  key: string;
  name: string;
  spdxId: string;
  url?: string; // Assuming url can be optional
  nodeId: string;
}
