import { IProfileTab } from "../types/profile-tab";

export const EMAIL_REGEX = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
export const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/

export const PROFILE_TABS : IProfileTab[] = [
    { label: "Posts", value: 1, icon: "fa-solid fa-table-cells-large" },
    { label: "Saved", value: 2, icon: "fa-regular fa-bookmark" },
]