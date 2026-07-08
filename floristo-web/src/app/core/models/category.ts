export interface StorefrontCategory {
    name: string;
    slug: string;
    imageUrl: string | null;
}

export interface StorefrontCategoryDetails {
    name: string;
    slug: string;
    parentSlug: string | null;
    description: string | null;
    imageUrl: string | null;
    seoTitle: string | null;
    seoDescription: string | null;
}

export interface CategoryNavigationItem {
    name: string;
    slug: string;
    imageUrl: string | null;
    children: CategoryNavigationItem[];
}

export interface TopLevelCategory {
    name: string;
    slug: string;
    imageUrl: string | null;
    shortDescription: string | null;
}