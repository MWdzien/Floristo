export interface ProductDimensions {
    height: number | null;
    width: number | null;
    depth: number | null;
    diameter: number | null;
    length: number | null;
}

export interface ProductImage {
    id: string;
    url: string;
    sortOrder: number;
}

export interface StorefrontProduct {
    name: string;
    slug: string;
    price: number;
    mainImageUrl: string;
}

export interface StorefrontProductDetails {
    id: string;
    name: string;
    slug: string;
    description: string | null;
    price: number;
    categorySlug: string;
    categoryName: string;
    dimensions: ProductDimensions | null;
    seoTitle: string | null;
    seoDescription: string | null;
    allowEngraving: boolean;
    allowGiftTag: boolean;
    images: ProductImage[];
}